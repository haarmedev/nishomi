using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
