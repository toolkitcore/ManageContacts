using ManageContacts.Service.Services.UploadFiles;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class UploadFileController : BaseController
{
    private readonly IUploadFileService _uploadFileService;
    public UploadFileController(ILogger<UploadFileController> logger, IUploadFileService uploadFileService) : base(logger)
    {
        _uploadFileService = uploadFileService;
    }
    
    [HttpPost]
    [Route("api/files/upload")]
    public async Task<IActionResult> UploadFile([FromForm]IFormFile file,
        CancellationToken cancellationToken = default)
        => Ok(await _uploadFileService.UploadFile(file, cancellationToken).ConfigureAwait(false));
    
    [HttpPost]
    [Route("api/files/uploads")]
    public async Task<IActionResult> UploadFiles([FromForm]IList<IFormFile> files,
        CancellationToken cancellationToken = default)
        => Ok(await _uploadFileService.UploadFiles(files, cancellationToken).ConfigureAwait(false));
}