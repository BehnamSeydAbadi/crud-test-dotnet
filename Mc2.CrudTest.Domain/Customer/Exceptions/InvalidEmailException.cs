using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class InvalidEmailException : AbstractDomainException
{
    public InvalidEmailException() : base(message: "Invalid email")
    {
    }
}