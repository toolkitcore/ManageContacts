using ManageContacts.Entity.Entities;
using ManageContacts.Model.Models.Users;
using ManageContacts.Service.Services.Users;
using ManageContacts.Shared.Consts;
using ManageContacts.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ManageContacts.WebApi.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(UserService));
    }
    
    #region [ADMIN PRIVATE API]
    [HttpGet]
    [Route("api/users")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> GetAllAsync([FromQuery]UserFilterRequestModel filter, CancellationToken cancellationToken = default) 
        => Ok(await _userService.GetAllAsync(filter, cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Route("api/users/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> GetAsync([FromRoute]Guid userId, CancellationToken cancellationToken = default) 
        => Ok(await _userService.GetAsync(userId, cancellationToken).ConfigureAwait(false));

    [HttpPost]
    [Route("api/users")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> CreateAsync([FromBody] UserEditModel userEdit,
        CancellationToken cancellationToken = default)
        => Ok(await _userService.CreateAsync(userEdit, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/users/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid userId,
        [FromBody] UserEditModel userEdit, CancellationToken cancellationToken = default)
        => Ok(await _userService.UpdateAsync(userId, userEdit, cancellationToken).ConfigureAwait(false));

    [HttpDelete]
    [Route("api/users/{id:guid}")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid userId,
        CancellationToken cancellationToken = default)
        => Ok(await _userService.DeleteAsync(userId, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/users/{id:guid}/recover")]
    [Authorized(Roles.Administrator, Roles.Manager)]
    public async Task<IActionResult> RecoverAsync([FromRoute(Name = "id")] Guid userId,
        CancellationToken cancellationToken = default)
        => Ok(await _userService.RecoverAsync(userId, cancellationToken).ConfigureAwait(false));
    #endregion
    
    #region [USER API]
    [AllowAnonymous]
    [HttpPost]
    [Route("api/users/sign_in")]
    public async Task<IActionResult> SignInAsync([FromBody]UserLoginModel userLogin, CancellationToken cancellationToken = default) 
        => Ok(await _userService.SignInAsync(userLogin, cancellationToken).ConfigureAwait(false));
    
    [AllowAnonymous]
    [HttpPost]
    [Route("api/users/sign_up")]
    public async Task<IActionResult> SignUpAsync([FromBody] UserRegistrationModel userRegister, CancellationToken cancellationToken = default) 
        => Ok(await _userService.SignUpAsync(userRegister, cancellationToken).ConfigureAwait(false));

    [AllowAnonymous]
    [HttpPut]
    [Route("api/users/forgot_password")]
    public async Task<IActionResult> ForgotPasswordAsync([FromQuery]string email, CancellationToken cancellationToken = default) 
        => Ok(await _userService.ForgotPasswordAsync(email, cancellationToken).ConfigureAwait(false));

    [HttpPut]
    [Route("api/users/refresh_token")]
    [Authorized]
    public async Task<IActionResult> RefreshTokenAsync(CancellationToken cancellationToken = default)
        => Ok(await _userService.RefreshTokenAsync(cancellationToken).ConfigureAwait(false));
    
    [HttpGet]
    [Route("api/users/me")]
    [Authorized]
    public async Task<IActionResult> GetProfileAsync(CancellationToken cancellationToken = default) 
        => Ok(await _userService.GetProfileAsync(cancellationToken).ConfigureAwait(false));
    
    [HttpPut]
    [Route("api/users/me/change_password")]
    [Authorized]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel changePassword, CancellationToken cancellationToken = default) 
        => Ok(await _userService.ChangePasswordAsync(changePassword, cancellationToken).ConfigureAwait(false));

    [HttpGet]
    [Route("api/users/confirm_email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery(Name = "user_id")]Guid userId, [FromQuery(Name = "code")]string code, CancellationToken cancellationToken)
        => Ok(await _userService.ConfirmEmailAsync(userId, code, cancellationToken).ConfigureAwait(false));
    
    [HttpPut]
    [Route("api/users/me")]
    [Authorized]
    public async Task<IActionResult> UpdateProfileAsync([FromBody]UserProfileEditModel userProfile, CancellationToken cancellationToken = default)
        => Ok(await _userService.UpdateProfileAsync(userProfile, cancellationToken).ConfigureAwait(false));
    #endregion

}