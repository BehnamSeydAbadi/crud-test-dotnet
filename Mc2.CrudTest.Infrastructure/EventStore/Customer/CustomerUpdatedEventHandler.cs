using Mc2.CrudTest.Domain.Customer.Events;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.EventStore.Customer;

public class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public CustomerUpdatedEventHandler(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _eventStoreRepository.AppendEventAsync(notification.AggregateId.ToString(), notification);
    }
}