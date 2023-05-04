using ManageContacts.Model.Abstractions;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Model.Models.Roles;
using ManageContacts.Service.Services.Roles;
using ManageContacts.Shared.Consts;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _roleService;
    public RoleController(ILogger<RoleController> logger, IRoleService roleService) : base(logger)
    {
        _roleService = roleService;
    }

    #region [ADMIN PRIVATE API]

    [HttpGet]
    [Route("api/roles")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> GetAllAsync([FromQuery] RoleFilterRequestModel filter, CancellationToken cancellationToken = default)
        => Ok(await _roleService.GetAllAsync(filter, cancellationToken).ConfigureAwait(false));

    [HttpGet]
    [Route("api/roles/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid userId, CancellationToken cancellationToken = default)
        => Ok(await _roleService.GetAsync(userId, cancellationToken).ConfigureAwait(false));

    [HttpPost]
    [Route("api/roles")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> CreateAsync([FromBody]RoleEditModel roleEdit, CancellationToken cancellationToken = default)
        => Ok(await _roleService.CreateAsync(roleEdit, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/roles/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")]Guid roleId, [FromBody]RoleEditModel roleEdit, CancellationToken cancellationToken = default)
        => Ok(await _roleService.UpdateAsync(roleId, roleEdit, cancellationToken).ConfigureAwait(false));

    [HttpDelete]
    [Route("api/roles/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]Guid roleId, CancellationToken cancellationToken = default) 
        => Ok(await _roleService.DeleteAsync(roleId, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/roles/{id:guid}/undelete")]
    public async Task<IActionResult> UndeleteAsync([FromRoute(Name = "id")] Guid roleId,
        CancellationToken cancellationToken)
        => Ok(await _roleService.UndeleteAsync(roleId, cancellationToken).ConfigureAwait(false));

    #endregion [ADMIN PRIVATE API]
}