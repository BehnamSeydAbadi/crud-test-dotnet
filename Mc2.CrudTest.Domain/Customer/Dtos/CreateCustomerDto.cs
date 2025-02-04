namespace Mc2.CrudTest.Domain.Customer.Dtos;

public class CreateCustomerDto : ICustomerBaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string BankAccountNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
}