using SoftProgres.PatientRegistry.Desktop.Models;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace SoftProgres.PatientRegistry.Desktop.Views.Pages;

public partial class PatientRegistryPage : INavigableView<ViewModels.PatientRegistryViewModel>
{
    public ViewModels.PatientRegistryViewModel ViewModel { get; }

    public PatientRegistryPage(ViewModels.PatientRegistryViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        viewModel.ErrorOccured += ViewModelOnErrorOccured;
        viewModel.PatientDeleted += ViewModelOnPatientDeleted;

        InitializeComponent();
    }
    
    private static void ViewModelOnPatientDeleted(object? sender, PatientOperationCompletedEventArgs e)
    {
        MessageBox.Show($"Bol zmazaný pacient s id {e.Patient?.Id}.", "Pacient bol zmazaný");
    }

    private static void ViewModelOnErrorOccured(object? sender, PatientOperationErrorEventArgs e)
    {
        MessageBox.Show(e.Message, e.Title);
    }
}