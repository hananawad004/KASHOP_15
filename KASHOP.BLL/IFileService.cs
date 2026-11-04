using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public interface IFileService
    {
        Task<string?> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
        void DeleteFile(string fileName);
    }
}
