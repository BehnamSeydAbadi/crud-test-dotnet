using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Events;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer.EventHandlers;

public class ProjectToReadDbOnCustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ICustomerRepository _customerRepository;

    public ProjectToReadDbOnCustomerCreatedEventHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        var customerDomainModel = CustomerDomainModel.Reconstruct(notification);
        await _customerRepository.AddAsync(customerDomainModel);
    }
}