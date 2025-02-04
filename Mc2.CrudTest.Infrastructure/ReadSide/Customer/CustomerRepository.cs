using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Domain.Customer.ValueObjects;
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

    public async Task<CustomerDomainModel[]> GetAsync()
    {
        var readModels = await _dbContext.Set<CustomerReadModel>().ToArrayAsync();

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


    protected virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}