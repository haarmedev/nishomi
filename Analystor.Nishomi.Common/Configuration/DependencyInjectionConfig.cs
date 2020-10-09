using Analystor.Nishomi.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Analystor.Nishomi.Common
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterCommonApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICategory, CategoryService>();
            services.AddSingleton<IProduct, ProductService>();
            services.AddSingleton<ICustomerRequest, CustomerService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton<SmtpProvider, SmtpProvider>();
        }
    }
}
