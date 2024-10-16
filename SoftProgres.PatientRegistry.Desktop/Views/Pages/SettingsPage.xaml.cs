using Wpf.Ui.Controls;

namespace SoftProgres.PatientRegistry.Desktop.Views.Pages;

public partial class SettingsPage : INavigableView<ViewModels.SettingsViewModel>
{
    public ViewModels.SettingsViewModel ViewModel { get; }

    public SettingsPage(ViewModels.SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}