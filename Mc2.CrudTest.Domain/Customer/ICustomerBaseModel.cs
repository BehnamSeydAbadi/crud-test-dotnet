namespace Mc2.CrudTest.Domain.Customer;

public interface ICustomerBaseModel
{
    string FirstName { get; }
    string LastName { get; }
    string PhoneNumber { get; }
    string Email { get; }
    string BankAccountNumber { get; }
    DateTime DateOfBirth { get; }
}