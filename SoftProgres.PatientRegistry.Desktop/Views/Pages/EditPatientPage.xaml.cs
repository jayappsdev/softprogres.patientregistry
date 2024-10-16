using SoftProgres.PatientRegistry.Desktop.Models;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace SoftProgres.PatientRegistry.Desktop.Views.Pages;

public partial class EditPatientPage : INavigableView<ViewModels.EditPatientViewModel>
{
    public ViewModels.EditPatientViewModel ViewModel { get; }

    public EditPatientPage(ViewModels.EditPatientViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        viewModel.ErrorOccured += ViewModelOnErrorOccured;
        viewModel.PatientCreated += ViewModelOnPatientCreated;
        viewModel.PatientUpdated += ViewModelOnPatientUpdated;
        
        InitializeComponent();
    }

    private static void ViewModelOnPatientUpdated(object? sender, PatientOperationCompletedEventArgs e)
    {
        MessageBox.Show($"Údaje pacienta s id {e.Patient?.Id} boli zmenené.", "Pacient bol aktualizovaný");
    }

    private static void ViewModelOnPatientCreated(object? sender, PatientOperationCompletedEventArgs e)
    {
        MessageBox.Show($"Bol vytvorený nový pacient s id {e.Patient?.Id}.", "Pacient bol pridaný");
    }

    private static void ViewModelOnErrorOccured(object? sender, PatientOperationErrorEventArgs e)
    {
        MessageBox.Show(e.Message, e.Title);
    }
}