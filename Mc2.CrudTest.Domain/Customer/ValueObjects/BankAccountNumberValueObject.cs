using Mc2.CrudTest.Domain.Customer.Exceptions;

namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record BankAccountNumberValueObject
{
    public static BankAccountNumberValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BankAccountNumberIsRequiredException();

        if (value.Length > 64 || value.All(c => char.IsDigit(c)) is false)
            throw new InvalidBankAccountNumberException();

        return new BankAccountNumberValueObject { Value = value };
    }

    public string Value { get; private set; }
}