using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analystor.Nishomi.Persistence
{
    public static class DBInitialization
    {
        public static void UpdateToLatestVersion(this NishomiDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
