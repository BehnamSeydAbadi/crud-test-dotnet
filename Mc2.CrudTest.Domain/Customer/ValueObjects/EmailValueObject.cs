using System.Net.Mail;
using Mc2.CrudTest.Domain.Customer.Exceptions;

namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record EmailValueObject
{
    public static EmailValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmailIsRequiredException();

        if (IsValidEmail(value) is false)
            throw new InvalidEmailException();

        return new EmailValueObject { Value = value };
    }

    private static bool IsValidEmail(string value)
    {
        try
        {
            _ = new MailAddress(value);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public string Value { get; private set; }
}