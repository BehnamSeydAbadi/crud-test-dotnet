using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Specifications;

public class GetByEmailSpecification : AbstractSpecification<CustomerDomainModel>
{
    public string Email { get; }

    public GetByEmailSpecification(string email)
    {
        Email = email;
    }
}