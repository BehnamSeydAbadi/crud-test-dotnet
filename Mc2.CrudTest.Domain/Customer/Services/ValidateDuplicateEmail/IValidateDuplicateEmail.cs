namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;

public interface IValidateDuplicateEmail
{
    void Validate(string email);
}