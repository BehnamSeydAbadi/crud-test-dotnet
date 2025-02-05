using Mc2.CrudTest.Application.Customer.Command.Exceptions;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Specifications;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.CreateCustomer.Behavior;

public class ValidateDuplicatePhoneNumberBehavior : IPipelineBehavior<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateDuplicatePhoneNumberBehavior(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(
        CreateCustomerCommand request, RequestHandlerDelegate<Guid> next, CancellationToken cancellationToken
    )
    {
        var anyCustomerWithSamePhoneNumber = await _customerRepository.AnyAsync(
            new GetByPhoneNumberSpecification(request.PhoneNumber)
        );

        if (anyCustomerWithSamePhoneNumber) throw new DuplicatePhoneNumberException();

        return await next();
    }
}