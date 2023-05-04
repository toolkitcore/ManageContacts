using System.Net;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Service.CacheServices.RoleCaches;
using ManageContacts.Service.Services.Users;
using ManageContacts.Shared.Consts;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManageContacts.WebApi.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizedAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;
    
    public AuthorizedAttribute(params string[] roles)
    {
        _roles = roles;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var httpContextAccessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as HttpContextAccessor
                                  ?? throw new ArgumentNullException(nameof(HttpContextAccessor));

        var roleCacheService = context.HttpContext.RequestServices.GetService(typeof(IRoleCacheService)) as RoleCacheService
                               ?? throw new ArgumentNullException(nameof(RoleCacheService));

        var userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as UserService
                             ?? throw new ArgumentNullException(nameof(UserService));
                        
        
        var currentUserId = httpContextAccessor.GetCurrentUserId() ;

        if (currentUserId == Guid.Empty)
        {
            context.Result = new JsonResult(new BaseResponseModel(HttpStatusCode.Unauthorized, "Unauthorized"));
            return;
        }

        var existUser = await userService.CheckExistUserAsync(currentUserId).ConfigureAwait(false);
        if (!existUser)
        {
            context.Result = new JsonResult(new BaseResponseModel(HttpStatusCode.Unauthorized, "Unauthorized"));
            return;
        }
            
        
        var userRoles = await roleCacheService.GetUserRolesAsync(currentUserId);
        
        if(_roles == null || _roles.Count() < 1) return;

        if(userRoles.Contains(Roles.Administrator)) return;
        
        foreach (var role in _roles)
        {
            if (userRoles.Contains(role))
            {
                return;
            }
        }
         
        context.Result = new JsonResult(new BaseResponseModel(HttpStatusCode.Unauthorized, "Unauthorized"));
        return;
        
    }
}
