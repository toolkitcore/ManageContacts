using Hangfire;
using ManageContacts.Service.HangfireService.ContactCleanupJob;
using ManageContacts.Service.HangfireService.FileCleanupJob;
using ManageContacts.WebApi.Middlewares;

namespace ManageContacts.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStaticFiles();
        
        app.UseHangfireDashboard();
        app.UseHangfireServer();
        app.UseJobHangfire();

        app.UseRouting();
        
        // app.UseHttpsRedirection(); // for production only
        
        app.UseAuthentication();

        app.UseExceptionMiddleware();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapHangfireDashboard();
        });
    }

    private static void UseJobHangfire(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<FileCleanupJob>("file-cleanup-job", job => job.Run(),
            "0 0 * * *");
        
        RecurringJob.AddOrUpdate<ContactCleanupJob>("contact-cleanup-job", job => job.Run(),
            "0 0 * * *");
    }
}
