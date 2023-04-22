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
        throw new NotImplementedException();
    }
}
