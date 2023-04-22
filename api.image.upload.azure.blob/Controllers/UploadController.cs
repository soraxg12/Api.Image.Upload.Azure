using api.image.upload.azure.blob.Dtos;
using api.image.upload.azure.blob.Models;
using api.image.upload.azure.blob.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.image.upload.azure.blob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ImageUploadService _imageUploadService;

        public UploadController()
        {
            _imageUploadService = new ImageUploadService(SettingsBlob.connectionString, SettingsBlob.container);
        }

        [HttpGet("get-image-filename/{fileName}")]
        public IActionResult GetImageForFileName(string fileName)
        {
            return Ok(_imageUploadService.GetImageForFileName(fileName));
        }
        
        [HttpGet("get-all-images-names")]
        public async Task<IActionResult> GetImageForFileName()
        {
            return Ok(await _imageUploadService.GetAllImage());
        }
        
        [HttpGet("get-all-uri-images")]
        public async Task<IActionResult> GetallImagesUri()
        {
            return Ok(await _imageUploadService.GetAllUriImages());
        }
        
        [HttpPost("upload-base64")]
        public IActionResult ImageBase64Upload([FromBody] ImageBase64Dto imageBase64Dto)
        {
            return Ok(_imageUploadService.UploadBase64(imageBase64Dto.imageBase64));
        }
        
        [HttpPost("upload-file")]
        public IActionResult ImageBase64Upload(IFormFile image)
        {
            string imageBase64 = ConvertBase64.ConvertImageToBase64(image);

            return Ok(_imageUploadService.UploadBase64(imageBase64));
        }
    }
}
