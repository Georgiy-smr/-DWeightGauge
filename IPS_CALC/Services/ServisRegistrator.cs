using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IPS_CALC.Services
{
    internal static class ServisRegistrator
    {
       public static IServiceCollection RegisterServis(
           this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialog>()
            ;
    }
}
