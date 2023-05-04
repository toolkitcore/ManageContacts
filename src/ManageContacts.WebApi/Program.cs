using ManageContacts.Infrastructure;
using ManageContacts.Shared.Serilog;
using ManageContacts.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start server Web API Manage Contacts.");

try
{
    builder.Host.UseSerilog(SeriLogger.Configure);

    builder.Host.AddAppConfigurations();
    
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    
    app.UseInfrastructure();

    app.AppMigrateDatabase().Run();

}
catch (Exception exception)
{
    Log.Fatal(exception, "Unhandled exception");
}
finally
{
    Log.Information("Shut dow web api Manage Contacts complete");
    Log.CloseAndFlush();
}

