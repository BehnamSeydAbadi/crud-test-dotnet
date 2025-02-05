using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class DuplicatePhoneNumberException : AbstractDomainException
{
    public DuplicatePhoneNumberException() : base(message: "Duplicate phone number")
    {
    }
}