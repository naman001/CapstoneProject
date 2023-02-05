using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public interface IMegaService
    {
        Task DeleteAsync(string url);
        Task<List<MegaIOModel>> GetListAsync(string folder);
        Task<string> UploadAsync(IFormFile file, string folder);
        Task<Stream> DownloadAsync(string url);
    }
}