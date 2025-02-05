using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class DuplicateCustomerException : AbstractDomainException
{
    public DuplicateCustomerException() : base(message: "Duplicate customer")
    {
    }
}