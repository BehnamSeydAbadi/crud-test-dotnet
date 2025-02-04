namespace Mc2.CrudTest.Domain.Common;

public abstract record AbstractDomainEvent
{
    public Guid AggregateId { get; }

    protected AbstractDomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}