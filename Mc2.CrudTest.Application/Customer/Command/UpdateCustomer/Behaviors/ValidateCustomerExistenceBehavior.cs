using Mc2.CrudTest.Application.Customer.Command.Exception;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Specifications;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Command.UpdateCustomer.Behaviors;

public class ValidateCustomerExistenceBehavior : IPipelineBehavior<UpdateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public ValidateCustomerExistenceBehavior(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(
        UpdateCustomerCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken
    )
    {
        var anyCustomer = await _customerRepository.AnyAsync(new GetByIdSpecification(request.Id));

        if (anyCustomer is false) throw new CustomerNotFoundException();

        return await next();
    }
}