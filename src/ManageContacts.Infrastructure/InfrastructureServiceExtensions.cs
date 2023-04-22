using ManageContacts.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ManageContacts.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}