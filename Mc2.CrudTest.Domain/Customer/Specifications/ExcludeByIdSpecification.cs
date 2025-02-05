using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Specifications;

public class ExcludeByIdSpecification : AbstractSpecification<CustomerDomainModel>
{
    public Guid Id { get; }


    public ExcludeByIdSpecification(Guid id)
    {
        Id = id;
    }
}