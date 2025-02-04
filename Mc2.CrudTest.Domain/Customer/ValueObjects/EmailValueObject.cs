namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record EmailValueObject
{
    public static EmailValueObject Create(string value)
    {
        return new EmailValueObject { Value = value };
    }

    public string Value { get; private set; }
}