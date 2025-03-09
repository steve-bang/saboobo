
using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.MigrationService;
using SaBooBo.UserService.Application.Auth;
using SaBooBo.UserService.Application.Clients;
using SaBooBo.UserService.Application.Identity;
using SaBooBo.UserService.Domain.Repositories;
using SaBooBo.UserService.Infrastructure;
using SaBooBo.UserService.Infrastructure.Auth;
using SaBooBo.UserService.Infrastructure.Clients;
using SaBooBo.UserService.Infrastructure.Repositories;

namespace SaBooBo.UserService.Extensions;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        // Add the database context
        builder.AddDatabaseContext();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Add the authentication method
        builder.AddAuthenticationMethod();

        // Add the user repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserExternalProviderRepository, UserExternalProviderRepository>();
        builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();

        return builder;
    }

    /// <summary>
    /// Add the database context to the application
    /// </summary>
    /// <param name="builder"></param>
     public static void AddDatabaseContext(this IHostApplicationBuilder builder)
     {
        // Add the DbContext
        builder.Services.AddDbContext<UserAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<UserAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<UserAppContext>();
     }

    /// <summary>
    /// Add the authentication method to the application
    /// </summary>
    /// <param name="builder">The host builder</param>
    /// <exception cref="NotImplementedException"></exception>
     public static void AddAuthenticationMethod(this IHostApplicationBuilder builder)
     {
                // Add the JWT settings
        var jwtSettings = builder.Configuration.GetSection("Identity");
        if(!jwtSettings.Exists())
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");    
        }

        var jwtSettingsValue = jwtSettings.Get<JwtSettings>();

        if(jwtSettingsValue == null)
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");    
        }
        builder.Services.AddSingleton(jwtSettingsValue);

        // Add the JWT handler
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();

        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddScoped<IMerchantClient, MerchantClient>();

        builder.AddDefaultAuthentication();
     }

}