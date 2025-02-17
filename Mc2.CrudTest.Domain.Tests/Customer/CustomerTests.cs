using FluentAssertions;
using Mc2.CrudTest.Domain.Customer.Exceptions;
using Mc2.CrudTest.Domain.Customer.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.Customer;

public class CustomerTests
{
    [Theory(DisplayName = "Create phone number successfully")]
    [InlineData("(234) 567-8901")]
    [InlineData("567-890-1234")]
    [InlineData("901-234-5678")]
    public void CreatePhoneNumberSuccessfully(string value)
    {
        PhoneNumberValueObject.Create(value);
    }

    [Theory(DisplayName = "Create phone number with invalid values should throw an exception")]
    [InlineData("+1-123-456-7890")]
    [InlineData("345.678.9012")]
    [InlineData("456 789 0123")]
    [InlineData("+44 678 901 2345")]
    [InlineData("(890) 123-4567")]
    [InlineData("012.345.6789")]
    public void CreatePhoneNumberWithInvalidValuesShouldThrowException(string value)
    {
        var action = () => PhoneNumberValueObject.Create(value);

        action.Should().ThrowExactly<InvalidPhoneNumberException>();
    }

    [Fact(DisplayName = "Create phone number with no value should throw an exception")]
    public void CreatePhoneNumberWithNoValueShouldThrowAnException()
    {
        var action = () => PhoneNumberValueObject.Create(value: string.Empty);

        action.Should().ThrowExactly<PhoneNumberIsRequiredException>();
    }

    [Fact(DisplayName = "Create bank account number successfully")]
    public void CreateBankAccountNumberSuccessfully()
    {
        BankAccountNumberValueObject.Create("12345678");
    }

    [Theory(DisplayName = "Create bank account number with valid values should throw an exception")]
    [InlineData("123456789_")]
    [InlineData("a")]
    [InlineData("_")]
    [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void CreateBankAccountNumberWithInvalidValuesShouldThrowException(string value)
    {
        var action = () => BankAccountNumberValueObject.Create(value);

        action.Should().ThrowExactly<InvalidBankAccountNumberException>();
    }

    [Fact(DisplayName = "Create bank account number with no value should throw an exception")]
    public void CreateBankAccountNumberWithNoValueShouldThrowAnException()
    {
        var action = () => BankAccountNumberValueObject.Create(value: string.Empty);

        action.Should().ThrowExactly<BankAccountNumberIsRequiredException>();
    }

    [Fact(DisplayName = "Create email successfully")]
    public void CreateEmailSuccessfully()
    {
        EmailValueObject.Create("behnam@mail.com");
    }

    [Theory(DisplayName = "Create email with invalid values should throw an exception")]
    [InlineData("plainaddress")]
    [InlineData("@missingusername.com")]
    [InlineData("username@.com")]
    [InlineData("username@domain,com")]
    public void CreateEmailWithInvalidValuesShouldThrowException(string value)
    {
        var action = () => EmailValueObject.Create(value);

        action.Should().ThrowExactly<InvalidEmailException>();
    }

    [Fact(DisplayName = "Create email with no value should throw an exception")]
    public void CreateEmailWithNoValueShouldThrowAnException()
    {
        var action = () => EmailValueObject.Create(value: string.Empty);

        action.Should().ThrowExactly<EmailIsRequiredException>();
    }

    [Theory(DisplayName = "There is some phone numbers, When creating a fixed line phone number, throw an exception")]
    [InlineData("+982188776655")]
    public void Something(string value)
    {
        var action = () => PhoneNumberValueObject.Create(value);
        action.Should().ThrowExactly<InvalidPhoneNumberException>();
    }
}