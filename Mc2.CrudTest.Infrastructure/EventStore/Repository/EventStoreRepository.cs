using System.Text;
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

    public async Task AppendEventAsync(string streamId, dynamic @event)
    {
        var eventData = new EventData(
            Uuid.NewUuid(),
            @event.GetType().AssemblyQualifiedName!,
            JsonSerializer.SerializeToUtf8Bytes(@event)
        );

        await _eventStoreClient.AppendToStreamAsync(streamId, expectedState: StreamState.Any, new[] { eventData });
    }

    public async Task<dynamic[]> GetEventsAsync(string streamId)
    {
        var result = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamId, StreamPosition.Start);

        var events = await result.ToListAsync();

        var domainEvents = new List<dynamic>();

        foreach (var resolvedEvent in events)
        {
            var eventType = Type.GetType(resolvedEvent.Event.EventType);

            if (eventType != null && typeof(AbstractDomainEvent).IsAssignableFrom(eventType))
            {
                var eventData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());

                var domainEvent = JsonSerializer.Deserialize(eventData, eventType);

                if (domainEvent != null)
                {
                    domainEvents.Add(domainEvent);
                }
            }
        }

        return domainEvents.ToArray();
    }
}