using Mc2.CrudTest.Application.Common;

namespace Mc2.CrudTest.Application.Customer.Command.Exceptions;

public class DuplicateEmailException : AbstractApplicationException
{
    public DuplicateEmailException() : base(message: "Duplicate email")
    {
    }
}