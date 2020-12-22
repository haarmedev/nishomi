﻿using Analystor.Nishomi.Domain;
using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace WebApplication2.IdentityServer
{
    public class DataSeeder
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(connectionString));
            services.AddDbContext<NishomiDbContext>(options =>
               options.UseMySql(connectionString));

            services.AddLogging(builder => builder.AddConsole());

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    var dbcontext = scope.ServiceProvider.GetService<NishomiDbContext>();

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                    if (dbcontext.Database.GetPendingMigrations().Any())
                    {
                        dbcontext.Database.Migrate();
                    }

                    //var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    //var admin = userMgr.FindByNameAsync("ahlan@nishomiabayas.com").Result;
                    //if (admin == null)
                    //{
                    //    admin = new User
                    //    {
                    //        UserName = "ahlan@nishomiabayas.com",
                    //        Email = "ahlan@nishomiabayas.com",
                    //        FirstName = "Super",
                    //        LastName = "Admin",
                    //        EmailConfirmed = true,
                    //        IsActive = true
                    //    };
                    //    var result = userMgr.CreateAsync(admin, "Pass123$").Result;
                    //    if (!result.Succeeded)
                    //    {
                    //        throw new Exception(result.Errors.First().Description);
                    //    }
                    //}
                }
            }
        }
    }
    }