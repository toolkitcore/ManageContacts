using System.Data.SqlClient;
using ManageContacts.Entity.Contexts;
using ManageContacts.Entity.Extensions;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Infrastructure.UnitOfWork;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ManageContacts.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetOptions<DatabaseSetting>(nameof(DatabaseSetting)) 
            ?? throw new ArgumentNullException(nameof(DatabaseSetting));

        var builder = new SqlConnectionStringBuilder(connectionString.Default);
        
        services.AddDbContext<ContactsContext>(options =>
        {
            options.UseSqlServer(builder.ConnectionString,
                options =>
                {
                    
                });
        });
        
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static IHost AppMigrateDatabase(this IHost host)
    {
        host.MigrateDatabase<ContactsContext>((context, _) =>
        {
            ContactsContextSeed.SeedAsync(context, Log.Logger).Wait();
        });
        return host;
    }
    
}