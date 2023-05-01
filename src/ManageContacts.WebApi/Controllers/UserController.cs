using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Service.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }
    
    #region [ADMIN PRIVATE API]
    [HttpGet]
    [Route("api/users")]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default) => null;
    
    
    [HttpGet]
    [Route("api/users/{id:guid}")]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default) => null;
    #endregion
    
    
    #region [USER PRIVATE API]
    [HttpPost]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInAsync(CancellationToken cancellationToken = default) => null;
    
    [HttpPost]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpAsync(CancellationToken cancellationToken = default) => null;
    
    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgotPasswordAsync(CancellationToken cancellationToken = default) => null;

    [HttpGet]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    [Route("api/users/profile")]
    public async Task<IActionResult> GetProfileAsync(CancellationToken cancellationToken = default) => null;
    
    [HttpPut]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    [Route("api/users/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(CancellationToken cancellationToken = default) => null;

    [HttpPut]
    [ProducesResponseType(typeof(OkResponseModel<BaseResponseModel>), StatusCodes.Status200OK)]
    [Route("api/users/profile")]
    public async Task<IActionResult> UpdateProfileAsync(CancellationToken cancellationToken = default) => null;
    
    #endregion

}