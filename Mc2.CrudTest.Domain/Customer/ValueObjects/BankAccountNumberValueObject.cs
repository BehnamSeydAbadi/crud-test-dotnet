namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record BankAccountNumberValueObject
{
    public static BankAccountNumberValueObject Create(string value)
    {
        return new BankAccountNumberValueObject { Value = value };
    }

    public string Value { get; private set; }
}