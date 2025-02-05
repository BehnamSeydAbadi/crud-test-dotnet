using Mc2.CrudTest.Application.Customer.Query.ViewModels;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Query.GetById;

public class GetCustomerByIdQuery : IRequest<CustomerViewModel?>
{
    public Guid Id { get; }

    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}