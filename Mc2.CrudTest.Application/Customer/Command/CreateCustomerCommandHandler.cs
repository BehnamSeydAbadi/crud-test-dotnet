using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Dtos;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command;

public class CreateCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<CreateCustomerCommand, Guid>
{
    public CreateCustomerCommandHandler(IMediator mediator) : base(mediator)
    {
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDomainModel = CustomerDomainModel.Create(new CreateCustomerDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            BankAccountNumber = request.BankAccountNumber,
            DateOfBirth = request.DateOfBirth,
        });

        var domainEventsQueue = customerDomainModel.GetQueuedDomainEvents();

        await PublishDomainEventsAsync(domainEventsQueue, cancellationToken);

        return customerDomainModel.AggregateId;
    }
}