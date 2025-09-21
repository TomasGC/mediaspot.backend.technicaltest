namespace Mediaspot.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void Raise(IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RemoveEvent(IDomainEvent domainEvent) => _domainEvents.RemoveAll(d => d.Id == domainEvent.Id);
}
