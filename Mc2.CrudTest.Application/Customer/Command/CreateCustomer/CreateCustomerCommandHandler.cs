using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.CreateCustomer;

public class CreateCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IValidateDuplicateCustomerDomainService _validateDuplicateCustomerDomainService;
    private readonly IValidateDuplicateEmail _validateDuplicateEmail;
    private readonly IValidateDuplicatePhoneNumberDomainService _validateDuplicatePhoneNumberDomainService;

    public CreateCustomerCommandHandler(
        IMediator mediator,
        IValidateDuplicateCustomerDomainService validateDuplicateCustomerDomainService,
        IValidateDuplicateEmail validateDuplicateEmail,
        IValidateDuplicatePhoneNumberDomainService validateDuplicatePhoneNumberDomainService
    ) : base(mediator)
    {
        _validateDuplicateCustomerDomainService = validateDuplicateCustomerDomainService;
        _validateDuplicateEmail = validateDuplicateEmail;
        _validateDuplicatePhoneNumberDomainService = validateDuplicatePhoneNumberDomainService;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDomainModel = CustomerDomainModel.Create(
            new CustomerDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = request.DateOfBirth,
            },
            _validateDuplicateCustomerDomainService,
            _validateDuplicateEmail,
            _validateDuplicatePhoneNumberDomainService
        );

        var domainEventsQueue = customerDomainModel.GetQueuedDomainEvents();
        await PublishDomainEventsAsync(domainEventsQueue, cancellationToken);

        return customerDomainModel.AggregateId;
    }
}