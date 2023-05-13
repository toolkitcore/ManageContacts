using ManageContacts.Service.Services.PhoneTypes;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class ContactTypeController : BaseController
{
    private readonly IPhoneTypeService _phoneTypeService;

    public ContactTypeController(ILogger<ContactTypeController> logger, IPhoneTypeService phoneTypeService) : base(logger)
    {
        _phoneTypeService = phoneTypeService ?? throw new ArgumentNullException(nameof(phoneTypeService));
    }
    
    [HttpGet]
    [Authorized]
    [Route("api/system/phone_types")]
    public async Task<IActionResult> GetAllPhoneTypeAsync(CancellationToken cancellationToken = default) 
        => Ok(await _phoneTypeService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    
}