using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;

namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.InMemoryRepository;

public class InMemoryEventStoreRepository : IEventStoreRepository
{
    private readonly List<Event> _events = new();

    public async Task AppendEventAsync(string streamId, AbstractDomainEvent @event)
    {
        await Task.CompletedTask;
        _events.Add(new Event(streamId, @event));
    }

    public async Task<AbstractDomainEvent[]> GetEventsAsync(string streamId)
    {
        await Task.CompletedTask;
        return _events.Where(e => e.StreamId == streamId).Select(e => e.DomainEvent).ToArray();
    }


    private record Event(string StreamId, AbstractDomainEvent DomainEvent);
}