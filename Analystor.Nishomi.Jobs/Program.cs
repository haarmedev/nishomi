using Analystor.Nishomi.Jobs;
using MySql.Data.MySqlClient;
using System;

namespace Analystor.Nishomi.Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupJob job = new BackupJob();
            job.CreateBackUp();
            job.CreateResourceFileBackup();
        }
    }
}
