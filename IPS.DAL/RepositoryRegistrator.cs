using System;
using System.Collections.Generic;
using System.Text;
using IPS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IPS.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepository(this IServiceCollection services) 
        {
            return services
                .AddTransient<IRepository<IPS>, RepositoryIPS>()
                .AddTransient<IRepository<Cargo>, RepositoryCagro>();
        }
    }
}
