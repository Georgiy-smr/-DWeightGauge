using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace IPS_CALC
{
    internal static class program
    {
        [STAThread]
        public static void Main()
        {
            IPS_CALC.App app = new IPS_CALC.App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices);
 
    }
}
