using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Specifications;

public class GetByFirstNameAndLastNameAndDateOtBirthSpecification : AbstractSpecification<CustomerDomainModel>
{
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateOfBirth { get; }

    public GetByFirstNameAndLastNameAndDateOtBirthSpecification(string firstName, string lastName, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
}