using Mc2.CrudTest.Application.Customer.Query.ViewModels;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.Specifications;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Query.GetById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = (await _customerRepository.GetAsync(new GetByIdSpecification(request.Id))).SingleOrDefault();

        return customer is null
            ? null
            : new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber.Value,
                Email = customer.Email.Value,
                BankAccountNumber = customer.BankAccountNumber.Value,
            };
    }
}