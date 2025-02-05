using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.ValueObjects;
using Mc2.CrudTest.Infrastructure.ReadSide.Customer.SpecificationHandlers;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer;

internal class CustomerRepository : ICustomerRepository
{
    protected readonly Mc2CrudTestDbContext _dbContext;

    public CustomerRepository(Mc2CrudTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CustomerDomainModel domainModel)
    {
        _dbContext.Set<CustomerReadModel>().Add(new CustomerReadModel
        {
            Id = domainModel.AggregateId,
            FirstName = domainModel.FirstName,
            LastName = domainModel.LastName,
            PhoneNumber = domainModel.PhoneNumber.Value,
            Email = domainModel.Email.Value,
            BankAccountNumber = domainModel.BankAccountNumber.Value,
            DateOfBirth = domainModel.DateOfBirth,
        });

        await SaveChangesAsync();
    }

    public async Task UpdateAsync(CustomerDomainModel domainModel)
    {
        var readModel = await _dbContext.Set<CustomerReadModel>()
            .SingleAsync(c => c.Id == domainModel.AggregateId);

        readModel.FirstName = domainModel.FirstName;
        readModel.LastName = domainModel.LastName;
        readModel.PhoneNumber = domainModel.PhoneNumber.Value;
        readModel.Email = domainModel.Email.Value;
        readModel.BankAccountNumber = domainModel.BankAccountNumber.Value;
        readModel.DateOfBirth = domainModel.DateOfBirth;
        _dbContext.Set<CustomerReadModel>().Update(readModel);

        await SaveChangesAsync();
    }

    public async Task<CustomerDomainModel[]> GetAsync(
        params AbstractSpecification<CustomerDomainModel>[] specifications)
    {
        var queryable = _dbContext.Set<CustomerReadModel>().AsQueryable();

        queryable = queryable.Apply(specifications);

        var readModels = await queryable.ToArrayAsync();

        return readModels.Select(c => new CustomerDomainModel
        {
            AggregateId = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            PhoneNumber = PhoneNumberValueObject.Create(c.PhoneNumber),
            Email = EmailValueObject.Create(c.Email),
            BankAccountNumber = BankAccountNumberValueObject.Create(c.BankAccountNumber),
            DateOfBirth = c.DateOfBirth,
        }).ToArray();
    }

    public async Task<bool> AnyAsync(params AbstractSpecification<CustomerDomainModel>[] specifications)
    {
        var queryable = _dbContext.Set<CustomerReadModel>().AsQueryable();

        queryable = queryable.Apply(specifications);

        return await queryable.AnyAsync();
    }

    public bool Any(params AbstractSpecification<CustomerDomainModel>[] specifications)
    {
        var queryable = _dbContext.Set<CustomerReadModel>().AsQueryable();

        queryable = queryable.Apply(specifications);

        return queryable.Any();
    }


    protected virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}