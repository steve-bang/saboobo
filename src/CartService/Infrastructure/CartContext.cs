
using Microsoft.EntityFrameworkCore;
using SaBooBo.CartService.Domain.AggregatesModel;

namespace SaBooBo.CartService.Infrastructure
{
    /// <summary>
    /// 
    /// Exec add migration: dotnet ef migrations add <Message> --context CartAppContext
    /// </summary>
    /// <param name="options"></param>
    public class CartAppContext(
        DbContextOptions<CartAppContext> options,
        IMediator _mediator
    ) : DbContext(options), IUnitOfWork
    {

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the default schema
            modelBuilder.HasDefaultSchema("SaBooBo");

            // Apply the configurations from the assembly
            modelBuilder
                    .Ignore<List<IDomainEvent>>()
                    .ApplyConfigurationsFromAssembly(typeof(CartAppContext).Assembly);

            base.OnModelCreating(modelBuilder);
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

            return true;
        }
    }
}