using Mc2.CrudTest.Application.Common;

namespace Mc2.CrudTest.Application.Customer.Command.Exception;

public class CustomerNotFoundException : AbstractApplicationException
{
    public CustomerNotFoundException() : base(message: "Customer not found")
    {
    }
}