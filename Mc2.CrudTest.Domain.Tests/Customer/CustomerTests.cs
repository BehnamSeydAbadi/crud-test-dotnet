using Mc2.CrudTest.Domain.Customer.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.Customer;

public class CustomerTests
{
    [Theory(DisplayName = "Create phone number successfully")]
    [InlineData("+1-123-456-7890")]
    [InlineData("(234) 567-8901")]
    [InlineData("345.678.9012")]
    [InlineData("456 789 0123")]
    [InlineData("567-890-1234")]
    [InlineData("+44 678 901 2345")]
    [InlineData("(890) 123-4567")]
    [InlineData("901-234-5678")]
    [InlineData("012.345.6789")]
    public void CreatePhoneNumberSuccessfully(string value)
    {
        PhoneNumberValueObject.Create(value);
    }
}