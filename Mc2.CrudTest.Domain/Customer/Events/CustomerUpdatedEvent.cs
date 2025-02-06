using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Domain.Customer.Events;

public class CustomerUpdatedEvent : AbstractDomainEvent, ICustomerBaseModel, INotification
{
    public override string ToString()
    {
        return
            $"{nameof(CustomerUpdatedEvent)}{AggregateId}{FirstName}{LastName}{PhoneNumber}{Email}{BankAccountNumber}{DateOfBirth}";
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}