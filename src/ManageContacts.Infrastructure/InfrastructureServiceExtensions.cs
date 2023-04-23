using ManageContacts.Entity.Contexts;
using ManageContacts.Infrastructure.Abstractions;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageContacts.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContactsContext>(options =>
        {
            options.UseSqlServer(configuration.GetOptions<DatabaseSetting>(nameof(DatabaseSetting)).Default);
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}