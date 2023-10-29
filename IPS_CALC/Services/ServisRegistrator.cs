using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Threading;
using IPS_CALC.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IPS_CALC.Services
{
    internal static class ServisRegistrator
    {
        public static IServiceCollection RegisterServis(
            this IServiceCollection services) => services
             .AddTransient<IUserDialog, UserDialog>()
             .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
             .AddSingleton<Dispatcher>(provider => Dispatcher.CurrentDispatcher)
             .AddSingleton<IEventService, EventService>()
            ;
    }
}
