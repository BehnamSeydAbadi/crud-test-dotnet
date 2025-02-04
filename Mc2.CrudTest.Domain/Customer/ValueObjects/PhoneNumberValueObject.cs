namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record PhoneNumberValueObject()
{
    public static PhoneNumberValueObject Create(string value)
    {
        return new PhoneNumberValueObject { Value = value };
    }

    public string Value { get; private set; }
}