using System.Net.Http.Json;
using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;
using Mc2.CrudTest.Application.Customer.Command;
using Mc2.CrudTest.Application.Customer.Query.ViewModels;
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

    public async Task CreateCustomerAsync(CreateCustomerCommand command)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.PostAsync("/api/customers", JsonContent.Create(command));

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var customerId = await httpResponseMessage.Content.ReadFromJsonAsync<Guid>();
            _scenarioContext.AddCustomerId(customerId);
        }
        else
        {
            var errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            _scenarioContext.AddCustomerErrorMessage(errorMessage);
        }
    }

    public async Task<CustomerViewModel?> GetCustomerAsync(Guid id)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.GetAsync($"/api/customers/{id}");

        return await httpResponseMessage.Content.ReadFromJsonAsync<CustomerViewModel>();
    }

    public async Task AssertCustomerCreatedSuccessfullyAsync()
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        var customerId = _scenarioContext.GetCustomerId();

        var customerReadModel = await dbContext.Set<CustomerReadModel>().SingleOrDefaultAsync(c => c.Id == customerId);

        customerReadModel.Should().NotBeNull();

        var command = _scenarioContext.GetCreateCustomerCommand();

        customerReadModel!.FirstName.Should().Be(command.FirstName);
        customerReadModel.LastName.Should().Be(command.LastName);
        customerReadModel.DateOfBirth.Should().Be(command.DateOfBirth);
        customerReadModel.PhoneNumber.Should().Be(command.PhoneNumber);
        customerReadModel.Email.Should().Be(command.Email);
        customerReadModel.BankAccountNumber.Should().Be(command.BankAccountNumber);
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
        var customerReadModel = new CustomerReadModel
        {
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
}