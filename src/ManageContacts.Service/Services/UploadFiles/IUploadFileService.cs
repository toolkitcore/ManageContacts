using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Files;
using Microsoft.AspNetCore.Http;

namespace ManageContacts.Service.Services.UploadFiles;

public interface IUploadFileService
{
    Task<OkResponseModel<FileModel>> UploadFile(IFormFile file, CancellationToken cancellationToken = default);
    Task<OkResponseModel<IEnumerable<FileModel>>> UploadFiles(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default);
}