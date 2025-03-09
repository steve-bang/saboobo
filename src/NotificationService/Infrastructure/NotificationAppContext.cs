

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SaBooBo.Domain.Shared;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Infrastructure;

/// <summary>
/// 
/// Exec add migration: dotnet ef migrations add <Message> --context NotificationAppContext
/// </summary>
/// <param name="options"></param>
public class NotificationAppContext(
    DbContextOptions<NotificationAppContext> options,
    IMediator _mediator
) : DbContext(options), IUnitOfWork
{

    public DbSet<Channel> Channels { get; set; } = null!;
    public DbSet<ChannelConfig> ChannelConfigs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set the default schema
        modelBuilder.HasDefaultSchema("SaBooBo");

        // Apply the configurations from the assembly
        modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(NotificationAppContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        // Dispatch Domain Events collection.

        return true;
    }
}