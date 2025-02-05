using Mc2.CrudTest.Domain.Customer.Events;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.EventStore.Customer;

public class CustomerDeletedEventHandler : INotificationHandler<CustomerDeletedEvent>
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public CustomerDeletedEventHandler(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _eventStoreRepository.AppendEventAsync(notification.AggregateId.ToString(), notification);
    }
}