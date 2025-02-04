using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Application.Customer.Exceptions;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Specifications;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command;

public class CreateCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(IMediator mediator, ICustomerRepository customerRepository) : base(mediator)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        await ValidatePhoneNumberDuplication(request.PhoneNumber);

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

    private async Task ValidatePhoneNumberDuplication(string phoneNumber)
    {
        var customerWithSamePhoneNumber =
            (await _customerRepository.GetAsync(new GetByPhoneNumberSpecification(phoneNumber)))
            .SingleOrDefault();

        if (customerWithSamePhoneNumber is not null)
            throw new DuplicatePhoneNumberException();
    }
}