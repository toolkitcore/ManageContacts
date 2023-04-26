using ManageContacts.Service.AuthServices.AccessToken;

namespace ManageContacts.WebApi.Middlewares;

public class HandleJwtTokenMiddleware : IMiddleware
{
    private readonly IAccessTokenService _accessTokenService;
    
    public HandleJwtTokenMiddleware(IAccessTokenService accessTokenService)
    {
        _accessTokenService = accessTokenService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if(token == null) 
            AttachUserToContext(context, token);

        await next(context);
    }

    #region [PRIVATE METHOD]
    
    private void AttachUserToContext(HttpContext context, string token)
    {
        var auth = _accessTokenService.ParseJwtToken(token);
        context.Items["Token"] = token;
        context.Items["Auth"] = auth;
    }

    #endregion [PRIVATE METHOD]
}