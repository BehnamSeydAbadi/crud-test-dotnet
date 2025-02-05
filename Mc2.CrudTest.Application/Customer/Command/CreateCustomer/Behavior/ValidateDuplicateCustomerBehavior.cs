using Mc2.CrudTest.Application.Customer.Command.Exceptions;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Specifications;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.CreateCustomer.Behavior;

public class ValidateDuplicateCustomerBehavior : IPipelineBehavior<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateDuplicateCustomerBehavior(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(
        CreateCustomerCommand request, RequestHandlerDelegate<Guid> next, CancellationToken cancellationToken
    )
    {
        var anyCustomer = await _customerRepository.AnyAsync(
            new GetByFirstNameAndLastNameAndDateOtBirthSpecification(
                request.FirstName.Trim(),
                request.LastName.Trim(),
                request.DateOfBirth
            )
        );

        if (anyCustomer) throw new DuplicateCustomerException();

        return await next();
    }
}