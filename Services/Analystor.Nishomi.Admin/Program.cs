using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analystor.Nishomi.Admin.IdentityServer;
using Analystor.Nishomi.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Analystor.Nishomi.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DataSeeder.EnsureSeedData(host.Services);

            host.Run();
            //CreateHostBuilder(args).Build().SetupDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
