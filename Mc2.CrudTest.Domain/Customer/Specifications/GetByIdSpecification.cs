using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Specifications;

public class GetByIdSpecification : AbstractSpecification<CustomerDomainModel>
{
    public Guid Id { get; }

    public GetByIdSpecification(Guid id)
    {
        Id = id;
    }
}