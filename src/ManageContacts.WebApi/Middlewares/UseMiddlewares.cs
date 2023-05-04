namespace ManageContacts.WebApi.Middlewares;

public static class UseMiddlewares
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<HandleExceptionMiddleware>();
    }
}