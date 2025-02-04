using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Application.Common;

public abstract class AbstractCommandHandler
{
    private readonly IMediator _mediator;

    public AbstractCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task PublishDomainEventsAsync(
        Queue<AbstractDomainEvent> domainEventsQueue, CancellationToken cancellationToken
    )
    {
        foreach (var domainEvent in domainEventsQueue)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
            Console.WriteLine("Domain event published: " + domainEvent.GetType().Name);
        }
    }
}