using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Domain.Customer.Events;

public record CustomerUpdatedEvent(
    Guid AggregateId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    string BankAccountNumber,
    DateTime DateOfBirth
) : AbstractDomainEvent(AggregateId), ICustomerBaseModel, INotification;