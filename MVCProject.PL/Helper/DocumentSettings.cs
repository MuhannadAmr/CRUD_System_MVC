using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVCProject.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);
            using var FS = new FileStream(filePath, FileMode.Create);
            file.CopyTo(FS);
            return fileName;
        }
        public static void DeleteFile(string FileName, string FolderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName,FileName);

            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
