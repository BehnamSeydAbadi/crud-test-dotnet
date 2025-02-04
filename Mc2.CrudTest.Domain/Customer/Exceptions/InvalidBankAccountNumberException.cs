using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class InvalidBankAccountNumberException : AbstractDomainException
{
    public InvalidBankAccountNumberException() : base(message: "Invalid bank account number")
    {
    }
}