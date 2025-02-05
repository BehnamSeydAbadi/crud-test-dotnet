using System.Net.Http.Json;
using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;
using Mc2.CrudTest.Application.Customer.Command;
using Mc2.CrudTest.Application.Customer.Command.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;
using Mc2.CrudTest.Application.Customer.Query.ViewModels;
using Mc2.CrudTest.Domain.Customer.Events;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using Mc2.CrudTest.Infrastructure.ReadSide;
using Mc2.CrudTest.Infrastructure.ReadSide.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerDriver
{
    private readonly ScenarioContext _scenarioContext;

    public CustomerDriver(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    public async Task<CustomerReadModel> SeedCustomerAsync(string phoneNumber)
    {
        var customerReadModel = new CustomerReadModel
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = phoneNumber,
            BankAccountNumber = "121212",
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "john@doe.com",
        };

        await AddCustomerReadModelAsync(customerReadModel);

        return customerReadModel;
    }

    public async Task<CustomerReadModel> SeedCustomerAsync(CreateCustomerCommand command)
    {
        var customerId = Guid.NewGuid();

        await SeedCutomerToEventStore(command, customerId);
        return await SeedCustomerToReadDb(command, customerId);
    }

    public async Task<Result> CreateCustomerAsync(CreateCustomerCommand command)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.PostAsync("/api/customers", JsonContent.Create(command));

        CustomerReadModel? customerReadModel = null;
        string? errorMessage = null;

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var customerId = await httpResponseMessage.Content.ReadFromJsonAsync<Guid>();
            var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();
            customerReadModel = await dbContext.Set<CustomerReadModel>().FindAsync(customerId);
        }
        else
        {
            errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        return new Result(customerReadModel, errorMessage);
    }

    public async Task<CustomerViewModel?> GetCustomerAsync(Guid id)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.GetAsync($"/api/customers/{id}");

        return await httpResponseMessage.Content.ReadFromJsonAsync<CustomerViewModel>();
    }

    public async Task<Result> UpdateCustomerAsync(Guid id, UpdateCustomerCommand command)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.PutAsync($"/api/customers/{id}", JsonContent.Create(command));

        string? errorMessage = null;

        if (httpResponseMessage.IsSuccessStatusCode is false)
        {
            errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        return new Result(Data: null, errorMessage);
    }

    public async Task<Result> DeleteCustomerAsync(Guid id)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.DeleteAsync($"/api/customers/{id}");

        string? errorMessage = null;

        if (httpResponseMessage.IsSuccessStatusCode is false)
        {
            errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        return new Result(Data: null, errorMessage);
    }

    public async Task AssertCustomerCreatedSuccessfullyAsync(Guid id)
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        var customerReadModel = await dbContext.Set<CustomerReadModel>().SingleOrDefaultAsync(c => c.Id == id);

        customerReadModel.Should().NotBeNull();

        var command = _scenarioContext.GetCreateCustomerCommand();

        customerReadModel!.FirstName.Should().Be(command.FirstName);
        customerReadModel.LastName.Should().Be(command.LastName);
        customerReadModel.DateOfBirth.Should().Be(command.DateOfBirth);
        customerReadModel.PhoneNumber.Should().Be(command.PhoneNumber);
        customerReadModel.Email.Should().Be(command.Email);
        customerReadModel.BankAccountNumber.Should().Be(command.BankAccountNumber);
    }

    public async Task AssertCustomerUpdatedSuccessfullyAsync(Guid id, UpdateCustomerCommand command)
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        var customerReadModel = await dbContext.Set<CustomerReadModel>().SingleOrDefaultAsync(c => c.Id == id);

        customerReadModel.Should().NotBeNull();

        customerReadModel!.FirstName.Should().Be(command.FirstName);
        customerReadModel.LastName.Should().Be(command.LastName);
        customerReadModel.DateOfBirth.Should().Be(command.DateOfBirth);
        customerReadModel.PhoneNumber.Should().Be(command.PhoneNumber);
        customerReadModel.Email.Should().Be(command.Email);
        customerReadModel.BankAccountNumber.Should().Be(command.BankAccountNumber);
    }

    public async Task AssertCustomerDeletedSuccessfullyAsync(Guid id)
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        var customerReadModel = await dbContext.Set<CustomerReadModel>().SingleOrDefaultAsync(c => c.Id == id);

        customerReadModel.Should().BeNull();
    }

    public void AssertErrorMessage(string errorMessage)
    {
        string customerErrorMessage = _scenarioContext.GetCustomerErrorMessage();
        customerErrorMessage.Should().Be(errorMessage);
    }


    private async Task AddCustomerReadModelAsync(CustomerReadModel customerReadModel)
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();
        dbContext.Set<CustomerReadModel>().Add(customerReadModel);
        await dbContext.SaveChangesAsync();
    }

    private async Task<CustomerReadModel> SeedCustomerToReadDb(CreateCustomerCommand command, Guid id)
    {
        var customerReadModel = new CustomerReadModel
        {
            Id = id,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PhoneNumber = command.PhoneNumber,
            BankAccountNumber = command.BankAccountNumber,
            DateOfBirth = command.DateOfBirth,
            Email = command.Email,
        };

        await AddCustomerReadModelAsync(customerReadModel);

        return customerReadModel;
    }

    private async Task SeedCutomerToEventStore(CreateCustomerCommand command, Guid id)
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();
        var eventStoreRepository = serviceScope.ServiceProvider.GetRequiredService<IEventStoreRepository>();

        await eventStoreRepository.AppendEventAsync(
            id.ToString(),
            new CustomerCreatedEvent(
                id,
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.Email,
                command.BankAccountNumber,
                command.DateOfBirth
            )
        );
    }


    public record Result(object? Data, string? ErrorMessage);
}