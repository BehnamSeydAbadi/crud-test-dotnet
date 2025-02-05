using Mc2.CrudTest.Domain.Customer.Exceptions;
using Mc2.CrudTest.Domain.Customer.Specifications;

namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;

public class ValidateDuplicatePhoneNumberDomainService : IValidateDuplicatePhoneNumberDomainService
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateDuplicatePhoneNumberDomainService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void Validate(string phoneNumber)
    {
        var anyCustomerWithSamePhoneNumber = _customerRepository.Any(
            new GetByPhoneNumberSpecification(phoneNumber)
        );

        if (anyCustomerWithSamePhoneNumber) throw new DuplicatePhoneNumberException();
    }
}