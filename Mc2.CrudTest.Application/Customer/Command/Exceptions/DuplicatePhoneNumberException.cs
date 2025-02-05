using Mc2.CrudTest.Application.Common;

namespace Mc2.CrudTest.Application.Customer.Command.Exceptions;

public class DuplicatePhoneNumberException : AbstractApplicationException
{
    public DuplicatePhoneNumberException() : base(message: "Duplicate phone number")
    {
    }
}