using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public class FileService : IFileService
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private static readonly long MaxFileSize = 2 * 1024 * 1024; // 2 MB
        private static readonly string ImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");


        public async Task<string?> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file is null || file.Length == 0 || file.Length > MaxFileSize) return null;
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if(!AllowedExtensions.Contains(extension)) return null;
            var fileName = Guid.NewGuid().ToString() + extension;
            Directory.CreateDirectory(ImagesPath);
            var filePath = Path.Combine(ImagesPath, fileName);
            using var stream = File.Create(filePath);
            await file.CopyToAsync(stream, cancellationToken);

            return fileName;
        }
        public void DeleteFile(string fileName)
        {
           var filePath = Path.Combine(ImagesPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

       
    }
}
