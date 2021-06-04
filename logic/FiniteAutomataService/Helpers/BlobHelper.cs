using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Helpers
{
    public static class BlobHelper
    {
        public static async Task PublishFile(IConfiguration configuration,string fileName,string filePath)
        {
            var containerClient = new BlobContainerClient(
                configuration.GetConnectionString("BlobClient"), configuration.GetConnectionString("BlobContainer"));

            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            using FileStream uploadFileStream = File.OpenRead(filePath);
            await blobClient.UploadAsync(uploadFileStream, true);
        }
    }
}
