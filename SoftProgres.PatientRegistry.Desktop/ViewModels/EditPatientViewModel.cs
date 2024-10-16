using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using ServiceStack;
using SoftProgres.PatientRegistry.Api.ServiceModel;
using SoftProgres.PatientRegistry.Desktop.Config;
using SoftProgres.PatientRegistry.Desktop.Context;
using SoftProgres.PatientRegistry.Desktop.Helpers;
using SoftProgres.PatientRegistry.Desktop.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace SoftProgres.PatientRegistry.Desktop.ViewModels;

public partial class EditPatientViewModel(
    IOptions<AppConfig> appConfig,
    INavigationService navigationService,
    PatientContextProvider patientContextProvider) : ObservableObject, INavigationAware
{
    private bool _isInitialized;

    private readonly JsonServiceClient _jsonServiceClient = new(appConfig.Value.ApiUrlBase);

    public event EventHandler<PatientOperationErrorEventArgs>? ErrorOccured;
    public event EventHandler<PatientOperationCompletedEventArgs>? PatientCreated;
    public event EventHandler<PatientOperationCompletedEventArgs>? PatientUpdated;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoBackCommand))]
    [NotifyCanExecuteChangedFor(nameof(SavePatientCommand))]
    private bool _isLoading;

    [ObservableProperty]
    private Patient _currentPatient = new();

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }

        if (patientContextProvider.CurrentPatientId.HasValue)
        {
            _ = LoadPatientAsync(patientContextProvider.CurrentPatientId.Value);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }

    private async Task LoadPatientAsync(long patientId)
    {
        IsLoading = true;
        try
        {
            var patientResponse = await _jsonServiceClient.GetAsync(new GetPatient()
            {
                PatientId = patientId
            });

            CurrentPatient = patientResponse.Patient.FromServiceToLocal();
        }
        catch (Exception ex)
        {
            ErrorOccured?.Invoke(this, new PatientOperationErrorEventArgs()
            {
                Title = "Chyba pri načítaní pacienta.",
                Message = ex.Message
            });
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand(CanExecute = nameof(CanGoBack))]
    private void GoBack()
    {
        navigationService.GoBack();
    }

    private bool CanGoBack()
    {
        return !IsLoading;
    }

    [RelayCommand(CanExecute = nameof(CanSavePatient))]
    private async Task SavePatientAsync()
    {
        if (patientContextProvider.CurrentPatientId.HasValue)
        {
            CurrentPatient.Id = patientContextProvider.CurrentPatientId.Value;
            IsLoading = true;
            try
            {
                var updatePatientResponse = await _jsonServiceClient.PutAsync(new UpdatePatient()
                {
                    PatientId = patientContextProvider.CurrentPatientId.Value,
                    BirthNumber = CurrentPatient.BirthNumber,
                    FirstName = CurrentPatient.FirstName,
                    LastName = CurrentPatient.LastName,
                    Email = CurrentPatient.Email,
                    PhoneNumber = CurrentPatient.PhoneNumber,
                    StreetAndNumber = CurrentPatient.StreetAndNumber,
                    City = CurrentPatient.City,
                    PostalCode = CurrentPatient.PostalCode,
                    State = CurrentPatient.State,
                    Workplace = CurrentPatient.Workplace,
                });

                patientContextProvider.CurrentPatientId = updatePatientResponse.Patient.Id;
                patientContextProvider.PatientRegisterUpdated = true;

                PatientUpdated?.Invoke(this, new PatientOperationCompletedEventArgs()
                {
                    Patient = updatePatientResponse.Patient.FromServiceToLocal()
                });
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, new PatientOperationErrorEventArgs()
                {
                    Title = "Chyba pri aktualizácii pacienta.",
                    Message = ex.Message
                });
            }
            finally
            {
                IsLoading = false;
            }
        }
        else
        {
            IsLoading = true;
            try
            {
                var createPatientResponse = await _jsonServiceClient.PostAsync(new CreatePatient()
                {
                    BirthNumber = CurrentPatient.BirthNumber,
                    FirstName = CurrentPatient.FirstName,
                    LastName = CurrentPatient.LastName,
                    Email = CurrentPatient.Email,
                    PhoneNumber = CurrentPatient.PhoneNumber,
                    StreetAndNumber = CurrentPatient.StreetAndNumber,
                    City = CurrentPatient.City,
                    PostalCode = CurrentPatient.PostalCode,
                    State = CurrentPatient.State,
                    Workplace = CurrentPatient.Workplace,
                });

                patientContextProvider.CurrentPatientId = createPatientResponse.Patient.Id;
                patientContextProvider.PatientRegisterUpdated = true;

                PatientCreated?.Invoke(this, new PatientOperationCompletedEventArgs()
                {
                    Patient = createPatientResponse.Patient.FromServiceToLocal()
                });
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, new PatientOperationErrorEventArgs()
                {
                    Title = "Chyba pri vytváraní pacienta.",
                    Message = ex.Message
                });
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private bool CanSavePatient()
    {
        return !IsLoading;
    }
}