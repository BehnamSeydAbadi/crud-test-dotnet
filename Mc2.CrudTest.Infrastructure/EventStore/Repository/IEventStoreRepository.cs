using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Infrastructure.EventStore.Repository;

public interface IEventStoreRepository
{
    Task AppendEventAsync(string streamId, AbstractDomainEvent @event);
    Task<AbstractDomainEvent[]> GetEventsAsync(string streamId);
}