﻿using Analystor.Nishomi.Core;
using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Net.Mail;

namespace Analystor.Nishomi.Api
{
    public static class ServiceWrapper
    {
        public static void RegisterServiceDependencies(this Container container)
        {
            // Library

            container.Register<IHttpContextAccessor, HttpContextAccessor>(Lifestyle.Singleton);
            container.Register<NishomiDbContextProvider, NishomiDbContextProvider>(Lifestyle.Singleton);
            container.Register<ICategory, CategoryService>(Lifestyle.Singleton);
            container.Register<IProduct, ProductService>(Lifestyle.Singleton);
            container.Register<ICustomerRequest, CustomerService>(Lifestyle.Singleton);
            container.Register<IMailService, MailService>(Lifestyle.Singleton);

            var smtpProvider = CreateSMTPProvider(container);
            container.RegisterInstance(smtpProvider);
        }

        /// <summary>
        /// Creates the SMTP provider.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>
        /// SMTP Provider
        /// </returns>
        private static SmtpProvider CreateSMTPProvider(Container container)
        {
            var provider = new SmtpProvider
            {
                SMTPResolver = delegate
                {
                    return container.GetInstance<SmtpClient>();
                }
            };

            return provider;
        }
    }
}
