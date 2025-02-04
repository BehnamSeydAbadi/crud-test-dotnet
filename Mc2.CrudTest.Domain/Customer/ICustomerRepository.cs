namespace Mc2.CrudTest.Domain.Customer;

public interface ICustomerRepository
{
    Task AddAsync(CustomerDomainModel domainModel);
    Task<CustomerDomainModel[]> GetAsync();
}