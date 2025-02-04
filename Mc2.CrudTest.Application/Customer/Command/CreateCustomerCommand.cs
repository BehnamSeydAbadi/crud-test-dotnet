using Mc2.CrudTest.Domain.Customer;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command;

public record CreateCustomerCommand : IRequest<Guid>, ICustomerBaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string BankAccountNumber { get; set; }
}