using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Events;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer.EventHandlers;

public class ProjectToReadDbOnCustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    private readonly ICustomerRepository _customerRepository;

    public ProjectToReadDbOnCustomerUpdatedEventHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var customerDomainModel = CustomerDomainModel.Reconstruct(notification);
        await _customerRepository.UpdateAsync(customerDomainModel);
    }
}