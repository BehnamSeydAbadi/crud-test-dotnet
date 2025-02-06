using Mc2.CrudTest.Domain.Common;
using MediatR;

namespace Mc2.CrudTest.Domain.Customer.Events;

public class CustomerCreatedEvent : AbstractDomainEvent, ICustomerBaseModel, INotification
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(CustomerCreatedEvent)}{AggregateId}{FirstName}{LastName}{PhoneNumber}{Email}{BankAccountNumber}{DateOfBirth}";
    }
}