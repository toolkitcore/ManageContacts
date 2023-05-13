using ManageContacts.Model.Models.Contacts;
using ManageContacts.Service.Services.Contacts;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class ContactController : BaseController
{
    private readonly IContactService _contactService;
    public ContactController(ILogger<ContactController> logger, IContactService contactService) : base(logger)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
    }

    [HttpGet]
    [Route("api/contacts")]
    [Authorized]
    public async Task<IActionResult> GetAllAsync([FromQuery]ContactFilterRequestModel filter, CancellationToken cancellationToken = default)
        => Ok(await _contactService.GetAllAsync(filter, cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Route("api/contacts/bin")]
    [Authorized]
    public async Task<IActionResult> GetAllBinAsync([FromQuery]ContactFilterRequestModel filter, CancellationToken cancellationToken = default)
        => Ok(await _contactService.GetAllDeletedAsync(filter, cancellationToken).ConfigureAwait(false));

    
    [HttpGet]
    [Route("api/contacts/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")]Guid contactId, CancellationToken cancellationToken = default)
        => Ok(await _contactService.GetAsync(contactId, cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Route("api/contacts/groups/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> GetAllByGroupIdAsync([FromRoute(Name = "id")]Guid groupId, CancellationToken cancellationToken = default)
        => Ok(await _contactService.GetAllByGroupIdAsync(groupId, cancellationToken).ConfigureAwait(false));

    [HttpPost]
    [Route("api/contacts")]
    [Authorized]
    public async Task<IActionResult> CreateAsync([FromBody]ContactEditModel contactEdit, CancellationToken cancellationToken = default)
        => Ok(await _contactService.CreateAsync(contactEdit, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/contacts/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")]Guid contactId, [FromBody]ContactEditModel contactEdit,
        CancellationToken cancellationToken = default)
        => Ok(await _contactService.UpdateAsync(contactId, contactEdit, cancellationToken).ConfigureAwait(false));

    [HttpDelete]
    [Route("api/contacts/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid contactId,
        CancellationToken cancellationToken = default)
        => Ok(await _contactService.DeleteAsync(contactId, cancellationToken).ConfigureAwait(false));
    
    
    [HttpPut]
    [Route("api/contact/{id:guid}/recover")]
    [Authorized]
    public async Task<IActionResult> RecoverAsync([FromRoute(Name = "id")] Guid contactId,
        CancellationToken cancellationToken = default)
        => Ok(await _contactService.RecoverAsync(contactId, cancellationToken).ConfigureAwait(false));
}