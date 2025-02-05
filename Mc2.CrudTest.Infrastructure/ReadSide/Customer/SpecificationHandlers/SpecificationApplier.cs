using Mc2.CrudTest.Domain.Customer.Specifications;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer.SpecificationHandlers;

public static class SpecificationApplier
{
    public static IQueryable<CustomerReadModel> Apply(
        this IQueryable<CustomerReadModel> queryable, dynamic[] specifications
    )
    {
        foreach (var specification in specifications)
        {
            queryable = ApplySpecification(queryable, specification);
        }

        return queryable;
    }

    private static IQueryable<CustomerReadModel> ApplySpecification(
        IQueryable<CustomerReadModel> queryable,
        GetByPhoneNumberSpecification specification
    )
    {
        return queryable.Where(c => c.PhoneNumber == specification.PhoneNumber);
    }

    private static IQueryable<CustomerReadModel> ApplySpecification(
        IQueryable<CustomerReadModel> queryable,
        GetByFirstNameAndLastNameAndDateOtBirthSpecification specification
    )
    {
        return queryable.Where(
            c => c.FirstName == specification.FirstName
                 && c.LastName == specification.LastName
                 && c.DateOfBirth == specification.DateOfBirth
        );
    }
}