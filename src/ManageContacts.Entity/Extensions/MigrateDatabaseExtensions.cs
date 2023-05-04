using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ManageContacts.Entity.Extensions;

public static class MigrateDatabaseExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating sql database.");
                ExecuteMigrations<TContext>(context);
                logger.LogInformation("Migrated sql database.");
                InvokeSeeder(seeder, context, services);
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "An error occurred while migrating the sql database.");
            }
        }

        return host;
    }

    private static void ExecuteMigrations<TContext>(TContext context)
        where TContext : DbContext
    {
        context.Database.Migrate();
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services)
    {
        seeder(context, services);
    }
}