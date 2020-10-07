using Analystor.Nishomi.Api.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api
{
    public static class DataBaseConfig
    {
        public static IHost SetupDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                scope.InitializeDatabase();
            }

            return host;
        }
    }
}
