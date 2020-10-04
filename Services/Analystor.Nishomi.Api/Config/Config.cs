using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api.Config
{
    public static class Config
    {
        public static void RegisterApplicationDependencies(this Container container, IServiceCollection services, IConfiguration configuration)
        {
            // Registers Common Dependencies
            container.RegisterCommonApplicationDependencies(services, configuration);
        }

        /// <summary>
        /// Registers the framework dependencies.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterFrameworkDependencies(this Container container, IServiceCollection services, IConfiguration configuration)
        {
            // Cookie Policy
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            // MVC Core
           services.AddMvc();

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
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                       .AddControllerActivation()
                       .AddViewComponentActivation()
                       .AddPageModelActivation()
                       .AddTagHelperActivation();
            });

            // Registers Common Dependencies
            container.RegisterCommonFrameworkDependencies(services, configuration);
        }
    }
}
