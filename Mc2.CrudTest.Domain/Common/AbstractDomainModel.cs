namespace Mc2.CrudTest.Domain.Common;

public abstract class AbstractDomainModel
{
    private readonly Queue<AbstractDomainEvent> _domainEvents = new();

    protected void QueueDomainEvent(AbstractDomainEvent @event)
    {
        _domainEvents.Enqueue(@event);
    }


    public Queue<AbstractDomainEvent> GetQueuedDomainEvents() => _domainEvents;
}