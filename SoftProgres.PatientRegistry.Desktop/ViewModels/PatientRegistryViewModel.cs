using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using ServiceStack;
using SoftProgres.PatientRegistry.Api.ServiceModel;
using SoftProgres.PatientRegistry.Desktop.Config;
using SoftProgres.PatientRegistry.Desktop.Context;
using SoftProgres.PatientRegistry.Desktop.Helpers;
using SoftProgres.PatientRegistry.Desktop.Models;
using SoftProgres.PatientRegistry.Desktop.Views.Pages;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace SoftProgres.PatientRegistry.Desktop.ViewModels;

public partial class PatientRegistryViewModel(
    IOptions<AppConfig> appConfig,
    INavigationService navigationService,
    PatientContextProvider patientContextProvider) : ObservableObject, INavigationAware
{
    private bool _isInitialized;

    private readonly JsonServiceClient _jsonServiceClient = new(appConfig.Value.ApiUrlBase);
    
    public event EventHandler<PatientOperationErrorEventArgs>? ErrorOccured;
    public event EventHandler<PatientOperationCompletedEventArgs>? PatientDeleted;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RefreshDataCommand))]
    [NotifyCanExecuteChangedFor(nameof(AddPatientCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditPatientCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeletePatientCommand))]
    private bool _isLoading;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ExportToCsvCommand))]
    private ObservableCollection<Patient> _patients = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditPatientCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeletePatientCommand))]
    private Patient? _selectedPatient;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
            return;
        }

        if (patientContextProvider.PatientRegisterUpdated)
        {
            patientContextProvider.PatientRegisterUpdated = false;
            _ = RefreshDataAsync();
        }

        if (patientContextProvider.CurrentPatientId.HasValue)
        {
            SelectedPatient = Patients.FirstOrDefault(p => p.Id == patientContextProvider.CurrentPatientId.Value);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        _ = RefreshDataAsync();
        _isInitialized = true;
    }

    [RelayCommand(CanExecute = nameof(CanRefreshData))]
    private async Task RefreshDataAsync()
    {
        IsLoading = true;
        try
        {
            var getPatientsResponse = await _jsonServiceClient.GetAsync(new GetPatients());
            if (getPatientsResponse.Patients.Count > 0)
            {
                Patients = new ObservableCollection<Patient>(
                    getPatientsResponse.Patients.Select(patient => patient.FromServiceToLocal()));
            }
        }
        catch (Exception ex)
        {
            ErrorOccured?.Invoke(this, new PatientOperationErrorEventArgs()
            {
                Title = "Chyba pri načítaní pacientov.",
                Message = ex.Message
            });
        }
        finally
        {
            IsLoading = false;
        }
    }

    private bool CanRefreshData()
    {
        return !IsLoading;
    }
    
    [RelayCommand(CanExecute = nameof(CanAddPatient))]
    private void AddPatient()
    {
        patientContextProvider.CurrentPatientId = null;
        navigationService.Navigate(typeof(EditPatientPage));
    }

    private bool CanAddPatient()
    {
        return !IsLoading;
    }

    [RelayCommand(CanExecute = nameof(CanEditPatient))]
    private void EditPatient()
    {
        patientContextProvider.CurrentPatientId = SelectedPatient?.Id;
        navigationService.Navigate(typeof(EditPatientPage));
    }

    private bool CanEditPatient()
    {
        return !IsLoading && SelectedPatient != null;
    }

    [RelayCommand(CanExecute = nameof(CanDeletePatient))]
    private async Task DeletePatientAsync()
    {
        var shouldDelete = false;

        // TODO imeplementuje MessageBox, ktorý sa užívateľa spýta, či chce naozaj odstrániť pacienta.
        // nastavte premennú shouldDelete na true, ak užívateľ zaklikne "Áno".

        var result = MessageBox.Show($"Ste si istý, že naozaj chcete odstrániť pacienta {SelectedPatient.LastName} ?", "Potvrdenie vymazania",
                                    System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == System.Windows.MessageBoxResult.Yes)
        {
            shouldDelete = true;
        }

        if (shouldDelete)
        {
            IsLoading = true;
            try
            {
                var tmpPatient = SelectedPatient;
                await _jsonServiceClient.DeleteAsync(new DeletePatient()
                {
                    PatientId = SelectedPatient!.Id,
                });
                await RefreshDataAsync();
                SelectedPatient = Patients.FirstOrDefault();
                PatientDeleted?.Invoke(this, new PatientOperationCompletedEventArgs()
                {
                    Patient = tmpPatient
                });
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, new PatientOperationErrorEventArgs()
                {
                    Title = "Chyba pri mazaní pacienta.",
                    Message = ex.Message
                });
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private bool CanDeletePatient()
    {
        return !IsLoading && SelectedPatient != null;
    }

    [RelayCommand(CanExecute = nameof(CanExportToCsv))]
    private async Task ExportToCsvAsync()
    {
        // TODO Naprogramujte funkcionalitu exportu dát zoznamu pacientov do CSV.
        // Užívateľ si musí vedieť vybrať cieľový súbor cez systémový dialog.
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "CSV file (*.csv)|*.csv",
            FileName = "DataGridExport.csv"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                string csvContent = GenerateCsvContent();
                await WriteToFileAsync(saveFileDialog.FileName, csvContent);
                MessageBox.Show("Data exported successfully.", "Export to CSV", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export data. Error: {ex.Message}", "Export Error", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private string GenerateCsvContent()
    {
        StringBuilder csvData = new StringBuilder();

        csvData.AppendLine("Id,FirstName,LastName,State,Email");

        foreach (var item in Patients)
        {
            csvData.AppendLine($"{item.Id},{item.FirstName},{item.LastName}, {item.State}, {item.Email}");
        }

        return csvData.ToString();
    }

    private async Task WriteToFileAsync(string filePath, string content)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            await writer.WriteAsync(content);
        }
    }

    private bool CanExportToCsv()
    {
        return Patients.Count > 0;
    }
}