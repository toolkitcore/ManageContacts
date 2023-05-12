using ManageContacts.Model.Models.Groups;
using ManageContacts.Service.Services.Groups;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    public GroupController(ILogger<GroupController> logger, IGroupService groupService) : base(logger)
    {
        _groupService = groupService;
    }

    [HttpGet]
    [Route("api/groups")]
    [Authorized]
    public async Task<IActionResult> GetAllAsync(GroupFilterRequestModel filter, CancellationToken cancellationToken = default)
        => Ok(await _groupService.GetAllAsync(filter, cancellationToken).ConfigureAwait(false));

    [HttpGet]
    [Route("api/groups/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> GetAsync(Guid groupId, CancellationToken cancellationToken = default)
        => Ok(await _groupService.GetAsync(groupId, cancellationToken).ConfigureAwait(false));

    [HttpPost]
    [Route("api/groups")]
    [Authorized]
    public async Task<IActionResult> CreateAsync([FromBody]GroupEditModel groupEdit, CancellationToken cancellationToken = default)
        => Ok(await _groupService.CreateAsync(groupEdit, cancellationToken).ConfigureAwait(false));
    
    [HttpPut]
    [Route("api/groups/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid groupId, [FromBody]GroupEditModel groupEdit, CancellationToken cancellationToken = default)
        => Ok(await _groupService.UpdateAsync(groupId, groupEdit, cancellationToken).ConfigureAwait(false));

    [HttpDelete]
    [Route("api/groups/{id:guid}")]
    [Authorized]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid groupId,
        [FromQuery(Name = "delete_contacts")] bool deleteGroupContacts = false,
        CancellationToken cancellationToken = default)
        => Ok(await _groupService.DeleteAsync(groupId, deleteGroupContacts, cancellationToken).ConfigureAwait(false));

}