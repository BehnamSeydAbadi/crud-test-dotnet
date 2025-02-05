using Mc2.CrudTest.Application.Common;

namespace Mc2.CrudTest.Application.Customer.Command.Exceptions;

public class DuplicateCustomerException : AbstractApplicationException
{
    public DuplicateCustomerException() : base(message: "Duplicate customer")
    {
    }
}