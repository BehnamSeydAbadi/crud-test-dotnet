using System.Text.Json;
using EventStore.Client;
using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Infrastructure.EventStore.Repository;

internal class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreClient _eventStoreClient;

    public EventStoreRepository(EventStoreClient eventStoreClient)
    {
        _eventStoreClient = eventStoreClient;
    }

    public async Task AppendEventAsync(string streamId, AbstractDomainEvent @event)
    {
        var eventData = new EventData(
            Uuid.NewUuid(),
            @event.GetType().Name,
            JsonSerializer.SerializeToUtf8Bytes(@event)
        );

        await _eventStoreClient.AppendToStreamAsync(streamId, expectedState: StreamState.Any, new[] { eventData });
    }

    public async Task<AbstractDomainEvent[]> GetEventsAsync(string streamId)
    {
        var result = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamId, StreamPosition.Start);

        if (await result.ReadState == ReadState.StreamNotFound)
        {
            return Array.Empty<AbstractDomainEvent>();
        }

        var events = await result.ToListAsync();

        return events.Select(
            resolvedEvent =>
            {
                var eventByteArray = resolvedEvent.Event.Data.ToArray();
                return JsonSerializer.Deserialize<AbstractDomainEvent>(eventByteArray)!;
            }
        ).ToArray();
    }
}