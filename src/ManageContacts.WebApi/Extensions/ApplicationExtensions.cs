using ManageContacts.WebApi.Middlewares;

namespace ManageContacts.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStaticFiles();

        app.UseRouting();
        
        // app.UseHttpsRedirection(); // for production only
        
        app.UseAuthentication();

        app.UseExceptionMiddleware();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}