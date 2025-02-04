using System.Linq.Expressions;
using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Customer.Specifications;

public class GetByPhoneNumberSpecification : AbstractSpecification<CustomerDomainModel>
{
    public string PhoneNumber { get; }

    public GetByPhoneNumberSpecification(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}