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
            Connection connection = new Connection();
            MySqlConnection con = connection.GetConnection();
            string path = "Nishomi-Db/Nishomi-Backup-" + DateTime.Now+".sql";
            using (MySqlCommand cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    using (var ms = new MemoryStream())
                    {
                        cmd.Connection = con;
                        con.Open();
                        mb.ExportToMemoryStream(ms);
                        await helper.UploadFileToS3(path,ms);
                        con.Close();
                    }
                }
            }
        }

        public async void CreateResourceFileBackup()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            string resourcesPath = builder.Build().GetSection("FilePath").Value;
            using (var ms = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    string path = "Nishomi-Resources/Nishomi-Resources" + DateTime.Now+".zip";
                    zip.AddDirectory(@""+resourcesPath, "Resources");
                    zip.Save(ms);
                    await helper.UploadFileToS3(path,ms);
                }
            }
        }
    }
}
