namespace ManageContacts.WebApi.Middlewares;

public static class UseMiddlewares
{
    public static void UseExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<HandleExceptionMiddleware>();
    }
}