using Microsoft.Extensions.DependencyInjection;

namespace ManageContacts.Service;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();

        // services.AddFluentValidator();
        // services.AddAutoMapper();
        return services;
    }
}