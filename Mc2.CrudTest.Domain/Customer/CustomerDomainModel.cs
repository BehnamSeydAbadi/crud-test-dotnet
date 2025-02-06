using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Events;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;
using Mc2.CrudTest.Domain.Customer.ValueObjects;

namespace Mc2.CrudTest.Domain.Customer;

public class CustomerDomainModel : AbstractDomainModel
{
    public Guid AggregateId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public PhoneNumberValueObject PhoneNumber { get; set; }
    public EmailValueObject Email { get; set; }
    public BankAccountNumberValueObject BankAccountNumber { get; set; }
    public DateTime DateOfBirth { get; set; }


    public static CustomerDomainModel Create(
        CustomerDto dto,
        IValidateDuplicateCustomerDomainService validateDuplicateCustomerDomainService,
        IValidateDuplicateEmail validateDuplicateEmailDomainService,
        IValidateDuplicatePhoneNumberDomainService validateDuplicatePhoneNumberDomainService)
    {
        validateDuplicateCustomerDomainService.Validate(dto);
        validateDuplicateEmailDomainService.Validate(dto.Email);
        validateDuplicatePhoneNumberDomainService.Validate(dto.PhoneNumber);

        var customerDomainModel = new CustomerDomainModel();

        var @event = new CustomerCreatedEvent
        {
            AggregateId = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            BankAccountNumber = dto.BankAccountNumber,
            DateOfBirth = dto.DateOfBirth
        };

        customerDomainModel.Apply(@event);
        customerDomainModel.QueueDomainEvent(@event);

        return customerDomainModel;
    }

    public static CustomerDomainModel Reconstruct(params dynamic[] @events)
    {
        var domainModel = new CustomerDomainModel();

        domainModel.Apply(@events);

        return domainModel;
    }


    public void Update(
        CustomerDto dto,
        IValidateDuplicateCustomerDomainService validateDuplicateCustomerDomainService,
        IValidateDuplicateEmail validateDuplicateEmail,
        IValidateDuplicatePhoneNumberDomainService validateDuplicatePhoneNumberDomainService
    )
    {
        validateDuplicateCustomerDomainService.Validate(AggregateId, dto);
        validateDuplicateEmail.Validate(AggregateId, dto.Email);
        validateDuplicatePhoneNumberDomainService.Validate(AggregateId, dto.PhoneNumber);

        var @event = new CustomerUpdatedEvent
        {
            AggregateId = this.AggregateId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            BankAccountNumber = dto.BankAccountNumber,
            DateOfBirth = dto.DateOfBirth
        };

        Apply(@event);
        QueueDomainEvent(@event);
    }

    public void Delete()
    {
        var @event = new CustomerDeletedEvent { AggregateId = this.AggregateId };
        Apply(@event);
        QueueDomainEvent(@event);
    }


    private void Apply(params dynamic[] domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            When(domainEvent);
        }
    }

    private void When(CustomerCreatedEvent @event)
    {
        this.AggregateId = @event.AggregateId;
        this.FirstName = @event.FirstName;
        this.LastName = @event.LastName;
        this.PhoneNumber = PhoneNumberValueObject.Create(@event.PhoneNumber);
        this.Email = EmailValueObject.Create(@event.Email);
        this.BankAccountNumber = BankAccountNumberValueObject.Create(@event.BankAccountNumber);
        this.DateOfBirth = @event.DateOfBirth;
    }

    private void When(CustomerUpdatedEvent @event)
    {
        this.AggregateId = @event.AggregateId;
        this.FirstName = @event.FirstName;
        this.LastName = @event.LastName;
        this.PhoneNumber = PhoneNumberValueObject.Create(@event.PhoneNumber);
        this.Email = EmailValueObject.Create(@event.Email);
        this.BankAccountNumber = BankAccountNumberValueObject.Create(@event.BankAccountNumber);
        this.DateOfBirth = @event.DateOfBirth;
    }

    private void When(CustomerDeletedEvent @event)
    {
        this.AggregateId = @event.AggregateId;
    }
}