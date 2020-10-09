using Analystor.Nishomi.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Admin
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Registers the application dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Registers Common Dependencies
            services.RegisterCommonApplicationDependencies(configuration);
            services.RegisterDbContext(configuration);
        }

        /// <summary>
        /// Registers the framework dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterFrameworkDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});

            //services.AddControllers(options =>
            //{
            //    options.InputFormatters.Insert(0, new FileMediaFormatter<DetailDTO>());
            //})
            //        .ConfigureApiBehaviorOptions(options =>
            //        {
            //            options.SuppressModelStateInvalidFilter = true;
            //        })
            //        .AddNewtonsoftJson(options =>
            //        {
            //            // Use the default property (Pascal) casing
            //            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //        });

            // MVC Core
            services.AddMvc(config => config.ModelBinderProviders.Insert(0, new DataTableModelBinderProvider()));

            // Session
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            // Identity Authentication
            //services.AddIdentityServerAuthentication(configuration);

            // Session
            services.AddSession();

            // Wire Simple Injector
            //services.AddSimpleInjector(container, options =>
            //{
            //    options.AddAspNetCore()
            //           .AddControllerActivation()
            //           .AddViewComponentActivation()
            //           .AddPageModelActivation()
            //           .AddTagHelperActivation();
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
    }
}
