using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;

public class UpdateCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<UpdateCustomerCommand>
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IValidateDuplicateCustomerDomainService _validateDuplicateCustomerDomainService;
    private readonly IValidateDuplicateEmail _validateDuplicateEmail;
    private readonly IValidateDuplicatePhoneNumberDomainService _validateDuplicatePhoneNumberDomainService;

    public UpdateCustomerCommandHandler(
        IMediator mediator,
        IEventStoreRepository eventStoreRepository,
        IValidateDuplicateCustomerDomainService validateDuplicateCustomerDomainService,
        IValidateDuplicateEmail validateDuplicateEmail,
        IValidateDuplicatePhoneNumberDomainService validateDuplicatePhoneNumberDomainService
    ) : base(mediator)
    {
        _eventStoreRepository = eventStoreRepository;
        _validateDuplicateCustomerDomainService = validateDuplicateCustomerDomainService;
        _validateDuplicateEmail = validateDuplicateEmail;
        _validateDuplicatePhoneNumberDomainService = validateDuplicatePhoneNumberDomainService;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var domainEvents = await _eventStoreRepository.GetEventsAsync(request.Id.ToString());

        var domainModel = CustomerDomainModel.Reconstruct(domainEvents);

        domainModel.Update(
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

        var domainEventsQueue = domainModel.GetQueuedDomainEvents();
        await PublishDomainEventsAsync(domainEventsQueue, cancellationToken);
    }
}