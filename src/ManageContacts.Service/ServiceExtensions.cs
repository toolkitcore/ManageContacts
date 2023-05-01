using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManageContacts.Service;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();

        services.AddFluentValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;

            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
                
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        });;
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}