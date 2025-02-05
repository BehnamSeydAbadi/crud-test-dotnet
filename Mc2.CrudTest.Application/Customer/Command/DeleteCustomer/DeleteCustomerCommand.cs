using System.Security.Cryptography.X509Certificates;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public Guid Id { get; }

    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}