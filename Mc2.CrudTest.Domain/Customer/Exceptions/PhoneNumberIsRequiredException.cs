using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class PhoneNumberIsRequiredException : AbstractDomainException
{
    public PhoneNumberIsRequiredException() : base(message: "Phone number is required")
    {
    }
}