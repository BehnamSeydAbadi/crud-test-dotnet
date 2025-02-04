using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class InvalidPhoneNumberException : AbstractDomainException
{
    public InvalidPhoneNumberException() : base(message: "Invalid phone number")
    {
    }
}