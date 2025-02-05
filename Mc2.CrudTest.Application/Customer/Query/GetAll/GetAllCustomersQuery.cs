using Mc2.CrudTest.Application.Customer.Query.ViewModels;
using MediatR;

namespace Mc2.CrudTest.Application.Customer.Query.GetAll;

public class GetAllCustomersQuery : IRequest<CustomerViewModel[]>
{
}