using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api.Config
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Registers the application common dependencies.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterCommonApplicationDependencies(this Container container, IServiceCollection services, IConfiguration configuration)
        {
            //services.RegisterDbContext(configuration, container);

            //container.RegisterAspNetIdentityDependencies();
            container.RegisterServiceDependencies();
            container.Register<SmtpClient>(
                    () =>
                    {
                        //var config = container.GetInstance<IConfiguration>();

                        return new SmtpClient()
                        {
                            Host = configuration.GetValue<string>("Email:Smtp:Host"),
                            Port = configuration.GetValue<int>("Email:Smtp:Port"),
                            Credentials = new NetworkCredential(
                                    configuration.GetValue<string>("Email:Smtp:Username"),
                                    configuration.GetValue<string>("Email:Smtp:Password"))
                        };
                    },
                    Lifestyle.Scoped);
        }

        /// <summary>
        /// Registers the framework common dependencies.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterCommonFrameworkDependencies(this Container container, IServiceCollection services, IConfiguration configuration)
        {
            // Memory cache
            services.AddDistributedMemoryCache();

            // HTTP Context accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Mail Service
            services.AddScoped<SmtpClient>((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            config.GetValue<string>("Email:Smtp:Username"),
                            config.GetValue<string>("Email:Smtp:Password"))
                };
            });

            services.AddHttpContextAccessor();

            //services.ConfigureApplicationSettings(configuration);
        }
    }
}
