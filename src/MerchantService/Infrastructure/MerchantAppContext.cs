
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SaBooBo.MerchantService.Infrastructure;


/// <summary>
/// 
/// Exec add migration: dotnet ef migrations add <Message> --context MerchantAppContext
/// </summary>
/// <param name="options"></param>
public class MerchantAppContext(
    DbContextOptions<MerchantAppContext> options
) : DbContext(options), IUnitOfWork
{
    public DbSet<Merchant> Merchants { get; set; } = null!;

    public DbSet<Banner> Banners { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set the default schema
        modelBuilder.HasDefaultSchema("SaBooBo");

        // Apply the configurations from the assembly
        modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(MerchantAppContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}