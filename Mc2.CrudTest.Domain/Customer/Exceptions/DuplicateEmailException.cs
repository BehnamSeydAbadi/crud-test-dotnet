using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class DuplicateEmailException : AbstractDomainException
{
    public DuplicateEmailException() : base(message: "Duplicate email")
    {
    }
}