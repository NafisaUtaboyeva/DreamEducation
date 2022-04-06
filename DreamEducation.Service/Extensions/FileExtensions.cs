using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DreamEducation.Service.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> SaveFileAsync(Stream file, string fileName, IConfiguration config, IWebHostEnvironment env)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath;
            if (fileName.Contains(".mp4"))
                storagePath = config.GetSection("Storage:VideoUrl").Value;
            else if (fileName.Contains(".pdf") || fileName.Contains(".docs") || fileName.Contains(".txt"))
                storagePath = config.GetSection("Storage:DocumentUrl").Value;
            else
                storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            FileStream mainFile = File.Create(filePath);

            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return filePath;
        }
    }
}
