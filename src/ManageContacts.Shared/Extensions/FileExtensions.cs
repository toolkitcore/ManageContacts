using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ManageContacts.Shared.Extensions;

public static class FileExtensions
{
    public static async Task<string> SaveFileAsync(this IFormFile file, IWebHostEnvironment _env, string folder = "uploads")
    {
        if (file == null)
            throw new NullReferenceException("Invalid file upload");
        
        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
        string fileExtension = Path.GetExtension(file.FileName);
        string newFileName = fileName + "_" + Guid.NewGuid() + fileExtension;
        string filePath  = Path.Combine(_env.WebRootPath, folder, newFileName);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return filePath.Trim();
    }
    
    public static async Task<List<string>> SaveFilesAsync(this IEnumerable<IFormFile> files, IWebHostEnvironment _env, string folder = "uploads")
    {
        if (files == null || files?.Count() < 1)
            return default!;
        
        var listFilePath = new List<String>();
        try
        {
            foreach (var file in files)
            {
                var filePath = await file.SaveFileAsync(_env, folder);
                listFilePath.Add(filePath);
            }
        }
        catch
        {
            foreach (var filePath in listFilePath)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            throw new IOException("File upload failed"); 
        }
        return listFilePath;
    }
    
    public static void MoveFile(string sourcePath, string targetPath)
    {
        if (!File.Exists(sourcePath))
            throw new IOException("Source path does not exist"); 
        

        File.Move(sourcePath, targetPath);
        
    }

    public static void DeleteFile(this string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    
    public static void DeleteFiles(this List<string> listFilePath)
    {
        foreach (var filePath in listFilePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
    
    public static void DeleteFilesInDirectory(string path)
    {
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            File.Delete(file);
        }
    }
}