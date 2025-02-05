using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Customer;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;

public class UpdateCustomerCommand : IRequest, ICustomerBaseModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string BankAccountNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
}