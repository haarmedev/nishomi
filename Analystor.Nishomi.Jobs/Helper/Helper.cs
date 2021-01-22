using Amazon;
using Amazon.EC2.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Jobs
{
    public class Helper
    {
        public async Task UploadFileToS3(string path,MemoryStream newMemoryStream)
        {
            using (var client = new AmazonS3Client("AKIAXC4HZW6A3UB6MNWE", "LjjJ6G0JKNkSsqdrTWPew/VTNpfxyztS/Ib7HXv3", RegionEndpoint.APSouth1))
            {
                try
                {
                    TransferUtility utility = new TransferUtility(client);
                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                    request.BucketName = "nishomi-staging-backup";
                    request.Key = path;
                    request.InputStream = newMemoryStream;
                    utility.Upload(request);
                    Console.WriteLine("Upload completed");
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Some Error Occured" + e);
                }
            }
        }
    }
}
