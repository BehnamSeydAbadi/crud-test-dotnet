using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Exceptions;

public class BankAccountNumberIsRequiredException : AbstractDomainException
{
    public BankAccountNumberIsRequiredException() : base(message: "Bank Account Number is required")
    {
    }
}