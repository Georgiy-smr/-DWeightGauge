using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IPS.DAL.Context;
using IPS.DAL;

namespace IPS_CALC.Data
{
    static class DBRegistrator
    {
        public static IServiceCollection AddDataBase
            (this IServiceCollection services,
            IConfiguration Configuration) =>
            services.AddDbContext<DBContext>
            (opt =>
            {
                var type = Configuration["Type"];
                switch (type)
                {
                    case null:
                        throw new
                        InvalidOperationException("Не понятно");
                    case "MSSQL":
                        opt.UseSqlServer
                        (Configuration.GetConnectionString
                        (type));
                        break;
                    case "SQLite":
                        opt.UseSqlite
                        (Configuration.GetConnectionString
                        (type));
                        break;
                    case "InMemory":
                        opt.UseInMemoryDatabase("IPS.db");
                        break;
                    default:
                        throw new 
                        InvalidOperationException
                        ($"Тип подключения {type} не поддерживается");
                }
            })
            .AddTransient<DbInitializer>()
            .AddRepository()
            ;
    }
}
