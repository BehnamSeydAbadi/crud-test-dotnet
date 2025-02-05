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
        await _customerDriver.SeedCustomerAsync(phoneNumber);
    }

    [Given(@"there is an existing customer with the following details:")]
    public async Task GivenThereIsAnExistingCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetCreateCustomerCommand();
        await _customerDriver.SeedCustomerAsync(command);
    }


    [When(@"As an operator, I create the customer with the following details:")]
    public async Task WhenAsAAnOperatorICreateTheCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetCreateCustomerCommand();
        _scenarioContext.AddCreateCustomerCommand(command);

        await _customerDriver.CreateCustomerAsync(command);
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
}