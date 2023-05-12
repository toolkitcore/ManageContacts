using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Files;
using ManageContacts.Shared.Exceptions;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ManageContacts.Service.Services.UploadFiles;

public class UploadFileService : IUploadFileService
{
    private readonly IWebHostEnvironment _env;
    public UploadFileService(IWebHostEnvironment env)
    {
        _env = env;
    }
    
    public async Task<OkResponseModel<FileModel>> UploadFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length < 0)
            throw new BadRequestException("File upload invalid");

        var path = await file.SaveFileAsync(_env);

        return new OkResponseModel<FileModel>(new FileModel(){ FilePath = path });
    }

    public async Task<OkResponseModel<IEnumerable<FileModel>>> UploadFiles(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Count() < 0)
            throw new BadRequestException("File upload invalid");

        var paths = await files.SaveFilesAsync(_env);
        
        var fileModels = paths.Select(f => new FileModel() { FilePath = f });
        
        return new OkResponseModel<IEnumerable<FileModel>>(fileModels);
    }
}