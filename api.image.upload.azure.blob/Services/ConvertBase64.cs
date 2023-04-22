using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.image.upload.azure.blob.Services
{
    public class ConvertBase64
    {
        public static string ConvertImageToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
