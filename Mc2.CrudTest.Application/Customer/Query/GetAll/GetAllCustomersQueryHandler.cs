using Mc2.CrudTest.Application.Customer.Query.ViewModels;
using Mc2.CrudTest.Domain.Customer;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Query.GetAll;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, CustomerViewModel[]>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerViewModel[]> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAsync();
        
        return customers.Select(c => new CustomerViewModel
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            DateOfBirth = c.DateOfBirth,
            PhoneNumber = c.PhoneNumber.Value,
            Email = c.Email.Value,
            BankAccountNumber = c.BankAccountNumber.Value,
        }).ToArray();
    }
}