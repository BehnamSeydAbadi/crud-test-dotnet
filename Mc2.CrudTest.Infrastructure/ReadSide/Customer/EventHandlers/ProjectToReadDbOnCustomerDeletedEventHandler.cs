using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Events;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer.EventHandlers;

public class ProjectToReadDbOnCustomerDeletedEventHandler : INotificationHandler<CustomerDeletedEvent>
{
    private readonly ICustomerRepository _customerRepository;

    public ProjectToReadDbOnCustomerDeletedEventHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteAsync(notification.AggregateId);
    }
}