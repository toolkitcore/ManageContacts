using ManageContacts.Service.Services.AddressTypes;
using ManageContacts.Service.Services.EmailTypes;
using ManageContacts.Service.Services.PhoneTypes;
using ManageContacts.Service.Services.RelativeTypes;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class ContactTypeController : BaseController
{
    private readonly IEmailTypeService _emailTypeService;
    private readonly IPhoneTypeService _phoneTypeService;
    private readonly IAddressTypeService _addressTypeService;
    private readonly IRelativeTypeService _relativeTypeService;
    
    public ContactTypeController(ILogger<ContactTypeController> logger, IEmailTypeService emailTypeService, IPhoneTypeService phoneTypeService, IAddressTypeService addressTypeService, IRelativeTypeService relativeTypeService) : base(logger)
    {
        _emailTypeService = emailTypeService ?? throw new ArgumentNullException(nameof(emailTypeService));
        _phoneTypeService = phoneTypeService ?? throw new ArgumentNullException(nameof(phoneTypeService));
        _addressTypeService = addressTypeService ?? throw new ArgumentNullException(nameof(addressTypeService));
        _relativeTypeService = relativeTypeService ?? throw new ArgumentNullException(nameof(relativeTypeService));
    }
    
    [HttpGet]
    [Authorized]
    [Route("api/system/email_types")]
    public async Task<IActionResult> GetAllEmailTypeAsync(CancellationToken cancellationToken = default) 
        => Ok(await _emailTypeService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Authorized]
    [Route("api/system/phone_types")]
    public async Task<IActionResult> GetAllPhoneTypeAsync(CancellationToken cancellationToken = default) 
        => Ok(await _phoneTypeService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Authorized]
    [Route("api/system/address_types")]
    public async Task<IActionResult> GetAllAddressTypeAsync(CancellationToken cancellationToken = default) 
        => Ok(await _addressTypeService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Authorized]
    [Route("api/system/relative_types")]
    public async Task<IActionResult> GetAllRelativeTypeAsync(CancellationToken cancellationToken = default) 
        => Ok(await _relativeTypeService.GetAllAsync(cancellationToken).ConfigureAwait(false));
}