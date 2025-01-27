
using Microsoft.EntityFrameworkCore;
using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.Domain.Shared.Extensions;
using SaBooBo.MerchantService.Domain.Repositories;
using SaBooBo.MerchantService.Infrastructure;
using SaBooBo.MerchantService.Infrastructure.Repositories;
using SaBooBo.MigrationService;
using SaBooBo.UserService.Application.Identity;

namespace SaBooBo.MerchantService.Extensions;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddMerchantService(this IHostApplicationBuilder builder)
    {

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDbContext<MerchantAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<MerchantAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<MerchantAppContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        //builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();

        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.AddDefaultAuthentication();

        return builder;
    }

}