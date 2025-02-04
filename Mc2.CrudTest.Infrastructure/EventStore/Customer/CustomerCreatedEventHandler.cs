using Mc2.CrudTest.Domain.Customer.Events;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.EventStore.Customer;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public CustomerCreatedEventHandler(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _eventStoreRepository.AppendEventAsync(notification.AggregateId.ToString(), notification);
        Console.WriteLine("Domain event stored: " + notification.GetType().Name);
    }
}