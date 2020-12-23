using Analystor.Nishomi.Admin.Data;
using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Admin.IdentityServer
{
    public class DataSeeder
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
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

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var admin = userMgr.FindByNameAsync("ahlan@nishomiabayas.com").Result;
                if (admin == null)
                {
                    admin = new IdentityUser
                    {
                        UserName = "ahlan@nishomiabayas.com",
                        Email = "ahlan@nishomiabayas.com",
                        //FirstName = "Super",
                        //LastName = "Admin",
                        EmailConfirmed = true,
                        //IsActive = true
                    };
                    var result = userMgr.CreateAsync(admin, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
