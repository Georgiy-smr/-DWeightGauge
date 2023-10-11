using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IPS_CALC.VIewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection RegisterViewModel(
            this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            return services;
        }
    }
}
