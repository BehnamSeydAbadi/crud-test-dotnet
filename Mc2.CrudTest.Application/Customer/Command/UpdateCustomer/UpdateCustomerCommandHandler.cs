using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;

public class UpdateCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<UpdateCustomerCommand>
{
    private readonly IEventStoreRepository _eventStore;
    private readonly IValidateDuplicateCustomerDomainService _validateDuplicateCustomerDomainService;

    public UpdateCustomerCommandHandler(
        IMediator mediator,
        IEventStoreRepository eventStore,
        IValidateDuplicateCustomerDomainService validateDuplicateCustomerDomainService
    ) : base(mediator)
    {
        _eventStore = eventStore;
        _validateDuplicateCustomerDomainService = validateDuplicateCustomerDomainService;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var domainEvents = await _eventStore.GetEventsAsync(request.Id.ToString());

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
            _validateDuplicateCustomerDomainService);

        var domainEventsQueue = domainModel.GetQueuedDomainEvents();
        await PublishDomainEventsAsync(domainEventsQueue, cancellationToken);
    }
}