using System.Net;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Service.CacheServices.RoleCaches;
using ManageContacts.Shared.Consts;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManageContacts.WebApi.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;
    
    public AuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var httpContextAccessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as HttpContextAccessor ??
                                  throw new ArgumentNullException(nameof(HttpContextAccessor));

        var roleCacheService = (RoleCacheService)context.HttpContext.RequestServices.GetService(typeof(IRoleCacheService)) 
                               ?? throw new ArgumentNullException(nameof(RoleCacheService));

        var currentUserId = httpContextAccessor.GetCurrentUserId() ;

        if (currentUserId == Guid.Empty)
        {
            context.Result = new JsonResult(new BaseResponseModel(HttpStatusCode.Unauthorized, "Unauthorized"));
            return;
        }
        
        if(_roles == null || _roles.Count() < 1) return;
        
        var userRoles = await roleCacheService.GetUserRolesAsync(currentUserId);
        
        if(userRoles.Contains(Roles.Admin)) return;
        
        foreach (var role in _roles)
        {
            if (userRoles.Contains(role))
            {
                return;
            }
        }
         
        context.Result = new JsonResult(new BaseResponseModel(HttpStatusCode.Unauthorized, "Forbidden"));
        return;
        
    }
}
