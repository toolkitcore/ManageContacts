using Microsoft.AspNetCore.Hosting;

namespace ManageContacts.Service.HangfireService.FileCleanupJob;

public class FileCleanupJob
{
    private readonly string _uploadPath;
    private readonly IWebHostEnvironment _env;
    
    public FileCleanupJob(IWebHostEnvironment env)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    }
    
    public void Run()
    {
        var filesToDelete = Directory.GetFiles(_uploadPath);
        foreach (var file in filesToDelete)
        {
            File.Delete(file);
        }
    }
}