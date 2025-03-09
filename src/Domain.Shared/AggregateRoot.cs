
namespace SaBooBo.Domain.Shared;

public abstract class AggregateRoot : Entity
{
    /// <summary>
    /// The events that have been applied to the aggregate root.
    /// </summary>
    private readonly List<IDomainEvent> _events = new();

    /// <summary>
    /// The events that have been applied to the aggregate root.
    /// </summary>
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    /// <summary>
    /// Clear the events that have been applied to the aggregate root.
    /// </summary>
    public void ClearEvents()
    {
        _events.Clear();
    }

    /// <summary>
    /// Add a domain event to the aggregate root.
    /// </summary>
    /// <param name="domainEvent"></param>
    public void AddEvent(IDomainEvent domainEvent)
    {
        Console.WriteLine($"[x] [{DateTime.UtcNow}] Adding domain event: {domainEvent.GetType().Name} at file {nameof(AggregateRoot)}");

        _events.Add(domainEvent);
    }
}