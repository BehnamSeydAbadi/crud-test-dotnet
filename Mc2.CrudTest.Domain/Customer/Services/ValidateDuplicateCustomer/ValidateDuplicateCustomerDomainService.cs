using Mc2.CrudTest.Domain.Customer.Dtos;
using Mc2.CrudTest.Domain.Customer.Exceptions;
using Mc2.CrudTest.Domain.Customer.Specifications;

namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;

internal class ValidateDuplicateCustomerDomainService : IValidateDuplicateCustomerDomainService
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateDuplicateCustomerDomainService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void Validate(CustomerDto dto)
    {
        var anyCustomer = _customerRepository.Any(
            new GetByFirstNameAndLastNameAndDateOtBirthSpecification(dto.FirstName, dto.LastName, dto.DateOfBirth)
        );

        if (anyCustomer) throw new DuplicateCustomerException();
    }
}