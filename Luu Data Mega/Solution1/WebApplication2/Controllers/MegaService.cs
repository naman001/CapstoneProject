using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MegaService : IMegaService
    {
        private readonly IConfiguration _configuration;

        public MegaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<MegaIOModel>> GetListAsync(string folder)
        {
            var listFile = new List<MegaIOModel>();
            var megaClient = new MegaApiClient();

            await megaClient.LoginAsync(_configuration["MegaAPI.Email"], _configuration["MegaAPI.Password"]);

            IEnumerable<INode>? nodes = await megaClient.GetNodesAsync();

            var nodeFolder = nodes.SingleOrDefault(x => x.Type == NodeType.Directory && x.Name == folder);
            var nodeByFolder = await megaClient.GetNodesAsync(nodeFolder);
            var files = nodeByFolder.Where(x => x.Type == NodeType.File);

            foreach(var file in files)
            {
                var downloadUrl = await megaClient.GetDownloadLinkAsync(file);

                listFile.Add(new MegaIOModel()
                {
                    FileName = file.Name,
                    FileSize = FileHelper.ToFileSize(file.Size),
                    DownloadUrl = downloadUrl.ToString()
                });
            }
            return listFile;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            using var memoryStream = new MemoryStream();
            var megaClient = new MegaApiClient();

            await file.CopyToAsync(memoryStream);
            await megaClient.LoginAsync(_configuration["MegaAPI.Email"], _configuration["MegaAPI.Password"]);
            IEnumerable<INode>? nodes = await megaClient.GetNodesAsync();
            var root = nodes.First(x => x.Type == NodeType.Directory && x.Name == folder);

            if (root == null)
                throw new System.Exception("Mega folder is not found! Please try again.");
            var myFile = await megaClient.UploadAsync(memoryStream, file.FileName, root);
            var megaUrl = await megaClient.GetDownloadLinkAsync(myFile);
            await megaClient.LogoutAsync();

            return megaUrl.ToString();
        }

        public async Task<Stream> DownloadAsync(string url)
        {
            var megaClient = new MegaApiClient();
            await megaClient.LoginAnonymousAsync();

            var fileLink = new Url(url);
            var stream = await megaClient.DownloadAsync(fileLink);
            await megaClient.LogoutAsync();
            return stream;  
        }

        public async Task DeleteAsync(string url)
        {
            var megaClient = new MegaApiClient();
            await megaClient.LoginAsync(_configuration["MegaAPI.Email"], _configuration["MegaAPI.Password"]);
            var fileLink = new Url(url);
            var node = await megaClient.GetNodesFromLinkAsync(fileLink);
            IEnumerable<INode>? nodes = await megaClient.GetNodesAsync();
            var allFiles = nodes.Where(n => n.Type == NodeType.File).ToList();
            var myFile = allFiles.FirstOrDefault(x => x.Name == node.Name);
            await megaClient.DeleteAsync(myFile, false);
            await megaClient.LogoutAsync();

        }
    }
}
