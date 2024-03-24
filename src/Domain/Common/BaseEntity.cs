using System.ComponentModel.DataAnnotations.Schema;

namespace AGInventoryManagement.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; }

    private readonly List<BaseEvent> _domainEvents = [];

    protected BaseEntity(Guid id) => Id = id;

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected BaseEntity() { }
}
