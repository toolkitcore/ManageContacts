using ManageContacts.Model.Abstractions;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Service.Services.Roles;
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
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default) => null;

    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default) => null;

    [HttpPost]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken = default) => null;

    [HttpPut]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(CancellationToken cancellationToken = default) => null;

    [HttpDelete]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default) => null;

    #endregion [ADMIN PRIVATE API]
}