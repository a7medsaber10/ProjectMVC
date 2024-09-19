using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ProjectMVC.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get located folder path
            //string folderPath = "F:\\Ahmed Saber\\ROUTE\\Back End\\Assignments\\[7] Assignment MVC\\Project MVC\\ProjectMVC.PL\\ProjectMVC.PL\\wwwroot\\Files\\Images\\";
            //string folderPath = Directory.GetCurrentDirectory() + "wwwroot\\Files" + folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            // 2. Get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; // file name
            //string fileName = file.Name; // Extension (jpg, png, pdf, etc..)

            // 3. Get filePath [folderPath + fileName]
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Save file as streams
            var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            // 5. return file name
            return fileName;

        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
