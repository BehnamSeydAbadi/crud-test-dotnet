using Mc2.CrudTest.Domain.Customer;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer;

public class CustomerReadModel : ICustomerBaseModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}