

using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.MigrationService;
using SaBooBo.UserService.Domain.Repositories;
using SaBooBo.UserService.Infrastructure;
using SaBooBo.UserService.Infrastructure.Repositories;

namespace SaBooBo.UserService.Extensions;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {

        builder.Services.AddDbContext<UserAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<UserAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<UserAppContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        builder.Services.AddScoped<IUserReposiroty, UserRepository>();

        return builder;
    }

}