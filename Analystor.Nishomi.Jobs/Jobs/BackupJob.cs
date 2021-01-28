using Ionic.Zip;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Analystor.Nishomi.Jobs
{
    public class BackupJob
    {
        Helper helper = new Helper();
        public async void CreateBackUp()
        {
            Console.WriteLine("DB Back up Job Started");
            Connection connection = new Connection();
            MySqlConnection con = connection.GetConnection();
            string path = "Nishomi-Db/Nishomi-Backup_"+ DateTime.Now.ToString("dddd, dd MMMM yyyy") + ".sql";
            using (MySqlCommand cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    using (var ms = new MemoryStream())
                    {
                        cmd.Connection = con;
                        con.Open();
                        mb.ExportToMemoryStream(ms);
                        Console.WriteLine("in progress....");
                        await helper.UploadFileToS3(path,ms);
                        Console.WriteLine("DB Back up Job Finished");
                        con.Close();
                    }
                }
            }
        }

        public async void CreateResourceFileBackup()
        {
            Console.WriteLine("Resource File Back up Job Started");
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            string resourcesPath = builder.Build().GetSection("FilePath").Value;
            using (var ms = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    string path = "Nishomi-Resources/Nishomi-Resources_"+ DateTime.Now.ToString("dddd, dd MMMM yyyy") + ".zip";
                    zip.AddDirectory(@""+resourcesPath, "Resources");
                    zip.Save(ms);
                    Console.WriteLine("in progress....");
                    await helper.UploadFileToS3(path,ms);
                    Console.WriteLine("Resource File Back up Job Finished");
                }
            }
        }
    }
}
