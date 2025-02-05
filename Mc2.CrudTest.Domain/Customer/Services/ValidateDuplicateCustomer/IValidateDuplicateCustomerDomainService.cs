using Mc2.CrudTest.Domain.Customer.Dtos;

namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;

public interface IValidateDuplicateCustomerDomainService
{
    void Validate(CustomerDto dto);
}