
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace SaBooBo.Domain.Shared.Extentions;
public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync<T>(this IMediator mediator, T ctx)
        where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}