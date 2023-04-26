using ManageContacts.Shared.Serilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

Log.Information("Start server Web API Manage Contacts ");

