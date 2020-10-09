using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analystor.Nishomi.Common
{
    public static class DatabaseConfig
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NishomiDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("NishomiDbContext"));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<NishomiDbContextProvider, NishomiDbContextProvider>();
        }

        /// <summary>
        /// Setups the database.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns>
        /// IHost
        /// </returns>
        public static IHost SetupDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                scope.InitializeDatabase();
            }

            return host;
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="serviceScope">The service scope.</param>
        public static void InitializeDatabase(this IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<NishomiDbContext>();
            context.UpdateToLatestVersion();
        }
    }
}
