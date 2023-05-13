using System.Text;
using Hangfire;
using ManageContacts.Infrastructure;
using ManageContacts.Service;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using ManageContacts.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManageContacts.WebApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        
        var jwtSetting = configuration.GetOptions<JwtSetting>();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience
                };
            });

        services.AddSwagger();
        
        services.AddInfrastructureService(configuration);

        services.AddService();

        services.AddHangfire(configuration);
        
        services.AddTransient<HandleExceptionMiddleware>();
        
        return services;
    }
    
   
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manage API", Version = "v1" });
    
            // Configure Swagger to use the JWT bearer authentication scheme
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securityScheme);
    
            // Make Swagger require a JWT token to access the endpoints
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new string[] {}
                }
            });
        });
        return services;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSetting = configuration.GetOptions<DatabaseSetting>();
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(databaseSetting.Default));
        
        return services;
    }
}