using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api.image.upload.azure.blob.Services
{
    public class ImageUploadService
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly string connectionString;
        private readonly string container;

        public ImageUploadService(string connectionString, string container)
        {
            this.connectionString = connectionString;
            this.container = container;
            _blobContainerClient = new BlobContainerClient(connectionString, container);
        }

        public string UploadBase64(string imageBase64)
        {
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(imageBase64, "");

            byte[] imageBytes = Convert.FromBase64String(data);

            var blobClient = new BlobClient(connectionString,container, fileName);

            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<IList<BlobItem>> GetAllImage()
        {
            var blobNames = new List<BlobItem>();

            var result = _blobContainerClient.GetBlobsAsync();

            await foreach (BlobItem blobItem in result)
            {
                blobNames.Add(blobItem);
            }

            return blobNames;
        } 
        
        public async Task<IList<string>> GetAllUriImages()
        {
            var blobUris = new List<string>();

            var result = _blobContainerClient.GetBlobsAsync();

            await foreach (BlobItem blobItem in result)
            {
                blobUris.Add(GetImageForFileName(blobItem.Name));
            }

            return blobUris;
        }

        public string GetImageForFileName(string fileName)
        {
            var image = _blobContainerClient.GetBlobClient(fileName);

            return image.Uri.AbsoluteUri;
        }
    }
}
