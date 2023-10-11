using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IPS_CALC.VIewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainWindowViewModel =>
            App.Host.Services.GetRequiredService<MainViewModel>();
    }
}
