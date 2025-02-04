using Mc2.CrudTest.Domain.Customer.Exceptions;
using PhoneNumbers;

namespace Mc2.CrudTest.Domain.Customer.ValueObjects;

public record PhoneNumberValueObject()
{
    public static PhoneNumberValueObject Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new PhoneNumberIsRequiredException();

        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneNumberUtil.Parse(value, defaultRegion: "US");

        if (phoneNumberUtil.IsValidNumber(phoneNumber) is false)
            throw new InvalidPhoneNumberException();


        return new PhoneNumberValueObject { Value = value };
    }

    public string Value { get; private set; }
}