using Mc2.CrudTest.Domain.Customer.Exceptions;
using Mc2.CrudTest.Domain.Customer.Specifications;

namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;

public class ValidateDuplicateEmail : IValidateDuplicateEmail
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateDuplicateEmail(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void Validate(string email)
    {
        var anyCustomer = _customerRepository.Any(new GetByEmailSpecification(email));

        if (anyCustomer) throw new DuplicateEmailException();
    }

    public void Validate(Guid id, string email)
    {
        var anyCustomer = _customerRepository.Any(
            new GetByEmailSpecification(email),
            new ExcludeByIdSpecification(id)
        );

        if (anyCustomer) throw new DuplicateEmailException();
    }
}