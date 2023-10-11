using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IPS_CALC.Services
{
    internal static class ServisRegistrator
    {
       public static IServiceCollection RegisterServis(
           this IServiceCollection services)
        {
            return services;
        }
    }
}
