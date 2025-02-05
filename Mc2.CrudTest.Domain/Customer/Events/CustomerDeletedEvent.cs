using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Domain.Customer.Events;

public record CustomerDeletedEvent(Guid AggregateId) : AbstractDomainEvent(AggregateId), INotification;