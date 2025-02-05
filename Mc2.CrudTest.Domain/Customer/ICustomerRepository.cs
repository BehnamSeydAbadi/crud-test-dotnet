using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Customer.Specifications;

namespace Mc2.CrudTest.Domain.Customer;

public interface ICustomerRepository
{
    Task AddAsync(CustomerDomainModel domainModel);

    Task<CustomerDomainModel[]> GetAsync(params AbstractSpecification<CustomerDomainModel>[] specifications);

    Task<bool> AnyAsync(params AbstractSpecification<CustomerDomainModel>[] specifications);
    
    bool Any(params AbstractSpecification<CustomerDomainModel>[] specifications);

    Task UpdateAsync(CustomerDomainModel domainModel);
}