using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.image.upload.azure.blob.Models
{
    public class SettingsBlob
    {
        public static string connectionString { get; set; }
        public static string container { get; set; }
    }
}
