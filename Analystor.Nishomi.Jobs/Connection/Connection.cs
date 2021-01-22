using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace Analystor.Nishomi.Jobs
{
    public class Connection
    {
        public MySqlConnection GetConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string connectionString = builder.Build().GetConnectionString("NishomiDbContext");
            MySqlConnection con = new MySqlConnection(connectionString);
            return con;
        } 
    }
}
