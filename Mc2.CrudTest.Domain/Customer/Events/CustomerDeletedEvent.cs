using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Domain.Customer.Events;

public class CustomerDeletedEvent : AbstractDomainEvent, INotification
{
    public override string ToString()
    {
        return $"{nameof(CustomerDeletedEvent)}{AggregateId}";
    }
}