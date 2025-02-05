using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Infrastructure.ReadSide;
using Mc2.CrudTest.Infrastructure.ReadSide.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public sealed class CustomerStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly CustomerDriver _customerDriver;

    public CustomerStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _customerDriver = new CustomerDriver(scenarioContext);
    }


    [Given(@"there is an existing customer with the phone number ""(.*)""")]
    public async Task GivenAnExistingCustomerWithThePhoneNumber(string phoneNumber)
    {
        var customerReadModel = await _customerDriver.SeedCustomerAsync(phoneNumber);
        _scenarioContext.AddCustomerId(customerReadModel.Id);
    }

    [Given(@"there is an existing customer with the following details:")]
    public async Task GivenThereIsAnExistingCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetCreateCustomerCommand();
        var customerReadModel = await _customerDriver.SeedCustomerAsync(command);
        _scenarioContext.AddCustomerId(customerReadModel.Id);
    }


    [When(@"As an operator, I create the customer with the following details:")]
    public async Task WhenAsAAnOperatorICreateTheCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetCreateCustomerCommand();
        _scenarioContext.AddCreateCustomerCommand(command);

        var result = await _customerDriver.CreateCustomerAsync(command);

        if (result.Data is not null)
        {
            var customerId = (Guid)result.Data;
            _scenarioContext.AddCustomerId(customerId);
        }
        else
        {
            _scenarioContext.AddCustomerErrorMessage(result.ErrorMessage!);
        }
    }

    [When(@"As an operator, I update the customer with the following details:")]
    public async Task WhenAsAnOperatorIUpdateTheCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetUpdateCustomerCommand();
        _scenarioContext.AddUpdateCustomerCommand(command);

        var result = await _customerDriver.UpdateCustomerAsync(_scenarioContext.GetCustomerId(), command);

        if (string.IsNullOrWhiteSpace(result.ErrorMessage) is false)
        {
            _scenarioContext.AddCustomerErrorMessage(result.ErrorMessage!);
        }
    }


    [Then(@"the customer should be created successfully")]
    public async Task ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        await _customerDriver.AssertCustomerCreatedSuccessfullyAsync();
    }

    [Then(@"an error ""(.*)"" should be thrown")]
    public void ThenAnErrorShouldBeThrown(string errorMessage)
    {
        _customerDriver.AssertErrorMessage(errorMessage);
    }

    [Then(@"the customer should be updated successfully")]
    public async Task ThenTheCustomerShouldBeUpdatedSuccessfully()
    {
        var customerId = _scenarioContext.GetCustomerId();
        var command = _scenarioContext.GetUpdateCustomerCommand();
        await _customerDriver.AssertCustomerUpdatedSuccessfullyAsync(customerId, command);
    }
}