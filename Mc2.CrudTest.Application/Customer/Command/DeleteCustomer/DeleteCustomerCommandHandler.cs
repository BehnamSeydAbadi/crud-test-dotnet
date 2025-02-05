using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.DeleteCustomer;

public class DeleteCustomerCommandHandler : AbstractCommandHandler, IRequestHandler<DeleteCustomerCommand>
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public DeleteCustomerCommandHandler(IMediator mediator, IEventStoreRepository eventStoreRepository) : base(mediator)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var domainEvents = await _eventStoreRepository.GetEventsAsync(request.Id.ToString());

        var domainModel = CustomerDomainModel.Reconstruct(domainEvents);

        domainModel.Delete();

        await PublishDomainEventsAsync(domainModel.GetQueuedDomainEvents(), cancellationToken);
    }
}