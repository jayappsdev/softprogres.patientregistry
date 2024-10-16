using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftProgres.PatientRegistry.Desktop.Config;
using SoftProgres.PatientRegistry.Desktop.Context;
using SoftProgres.PatientRegistry.Desktop.Services;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Wpf.Ui;

namespace SoftProgres.PatientRegistry.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                var basePath =
                    Path.GetDirectoryName(AppContext.BaseDirectory)
                    ?? throw new DirectoryNotFoundException(
                        "Unable to find the base directory of the application."
                    );
                _ = c.SetBasePath(basePath).AddJsonFile("appsettings.json");
            })
            .ConfigureServices(
                (context, services) =>
                {
                    // App Host
                    _ = services.AddHostedService<ApplicationHostService>();

                    // Page resolver service
                    _ = services.AddSingleton<IPageService, PageService>();

                    // Theme manipulation
                    _ = services.AddSingleton<IThemeService, ThemeService>();

                    // TaskBar manipulation
                    _ = services.AddSingleton<ITaskBarService, TaskBarService>();

                    // Service containing navigation, same as INavigationWindow... but without window
                    _ = services.AddSingleton<INavigationService, NavigationService>();

                    // Main window with navigation
                    _ = services.AddSingleton<INavigationWindow, Views.MainWindow>();
                    _ = services.AddSingleton<ViewModels.MainWindowViewModel>();

                    // Views and ViewModels
                    _ = services.AddSingleton<Views.Pages.PatientRegistryPage>();
                    _ = services.AddSingleton<ViewModels.PatientRegistryViewModel>();
                    _ = services.AddSingleton<Views.Pages.EditPatientPage>();
                    _ = services.AddSingleton<ViewModels.EditPatientViewModel>();
                    _ = services.AddSingleton<Views.Pages.SettingsPage>();
                    _ = services.AddSingleton<ViewModels.SettingsViewModel>();

                    // Configuration
                    _ = services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
                    _ = services.AddSingleton<PatientContextProvider>();
                    
                }
            )
            .Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T? GetService<T>()
            where T : class
        {
            return Host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await Host.StartAsync();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await Host.StopAsync();

            Host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}