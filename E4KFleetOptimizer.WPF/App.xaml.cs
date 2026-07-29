using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Services;
using E4KFleetOptimizer.WPF.Data;
using E4KFleetOptimizer.WPF.Services;
using E4KFleetOptimizer.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace E4KFleetOptimizer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.AddSingleton<IShipDataProvider, JsonShipDataProvider>();
            services.AddSingleton<IFleetOptimizationService, FleetOptimizationService>();
            services.AddSingleton<IThemeService, WpfThemeService>();
            services.AddSingleton<IShipReferenceProvider, CsvShipReferenceProvider>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
