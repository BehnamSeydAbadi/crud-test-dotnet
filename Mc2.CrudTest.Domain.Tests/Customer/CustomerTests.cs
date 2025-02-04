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
}