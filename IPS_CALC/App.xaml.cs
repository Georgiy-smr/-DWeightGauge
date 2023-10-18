using IPS_CALC.Data;
using IPS_CALC.Services;
using IPS_CALC.VIewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IPS_CALC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window ActiveWindow =>
            Application.Current.Windows.OfType<Window>()
                                       .FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow =>
            Application.Current.Windows.OfType<Window>()
                                        .FirstOrDefault(w => w.IsFocused);

        public static Window CurrentWindow => ActiveWindow ?? FocusedWindow;


        private static IHost _Host;
        public static IHost Host => _Host
            ?? program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        internal static void ConfigureServices
            (HostBuilderContext host, IServiceCollection services) => services
            .RegisterViewModel()
            .RegisterServis()
            .AddDataBase(host.Configuration.GetSection("DataBase"))
            ;


        public static IServiceProvider Services => Host.Services;
        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            using (var scope = Services.CreateScope())
                await scope.ServiceProvider.
                    GetRequiredService<DbInitializer>().InitializeAsync();

            base.OnStartup(e);
            await host.RunAsync();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            var host = Host;
            base.OnExit(e);
            using(Host) await host.StopAsync();
        }
    }
}
