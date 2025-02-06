namespace Mc2.CrudTest.Domain.Common;

public abstract class AbstractDomainEvent
{
    public Guid AggregateId { get; set; }

    public abstract override string ToString();
}