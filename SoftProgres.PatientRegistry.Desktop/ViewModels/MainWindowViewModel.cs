using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace SoftProgres.PatientRegistry.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _navigationItems = [];

    [ObservableProperty]
    private ObservableCollection<object> _navigationFooter = [];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [];
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "Správa pacientov";
        
        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Pacienti",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Patient24 },
                TargetPageType = typeof(Views.Pages.PatientRegistryPage)
            },
        ];
        
        NavigationFooter =
        [
            new NavigationViewItem()
            {
                Content = "Nastavenia",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            },
        ];

        _isInitialized = true;
    }
}