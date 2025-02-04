using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class EmailIsRequiredException : AbstractDomainException
{
    public EmailIsRequiredException() : base(message: "Email is required")
    {
    }
}