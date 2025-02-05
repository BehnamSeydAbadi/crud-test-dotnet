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
        _scenarioContext.AddCustomers(customerReadModel);
    }

    [Given(@"there are existing customers with the following details:")]
    [Given(@"there is an existing customer with the following details:")]
    public async Task GivenThereAreExistingCustomersWithTheFollowingDetails(Table table)
    {
        var customers = new List<CustomerReadModel>();

        foreach (var tableRow in table.Rows)
        {
            var command = tableRow.GetCreateCustomerCommand();
            var customerReadModel = await _customerDriver.SeedCustomerAsync(command);
            customers.Add(customerReadModel);
        }

        _scenarioContext.AddCustomers(customers.ToArray());
    }


    [When(@"As an operator, I create the customer with the following details:")]
    public async Task WhenAsAAnOperatorICreateTheCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.Rows[0].GetCreateCustomerCommand();
        _scenarioContext.AddCreateCustomerCommand(command);

        var result = await _customerDriver.CreateCustomerAsync(command);

        if (result.Data is not null)
        {
            var customerReadModel = (CustomerReadModel)result.Data;
            _scenarioContext.AddCustomers(customerReadModel);
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

        var result = await _customerDriver.UpdateCustomerAsync(_scenarioContext.GetCustomers().Single().Id, command);

        if (string.IsNullOrWhiteSpace(result.ErrorMessage) is false)
        {
            _scenarioContext.AddCustomerErrorMessage(result.ErrorMessage!);
        }
    }

    [When(
        @"As an operator, I update the customer with first name ""(.*)"", last name ""(.*)"" and date of birth ""(.*)"" the following details:")]
    public async Task WhenAsAnOperatorIUpdateTheCustomerWithFirstNameLastNameAndDateOfBirthTheFollowingDetails(
        string firstName, string lastName, string dateOfBirth, Table table
    )
    {
        var dateOfBirthInDateTime = dateOfBirth.ToDateTime();

        var customerId = _scenarioContext.GetCustomers().Single(
            c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirthInDateTime
        ).Id;

        var result = await _customerDriver.UpdateCustomerAsync(customerId, table.GetUpdateCustomerCommand());

        if (string.IsNullOrWhiteSpace(result.ErrorMessage) is false)
        {
            _scenarioContext.AddCustomerErrorMessage(result.ErrorMessage);
        }
    }

    [When(@"As an operator, I delete the customer")]
    public async Task WhenAsAnOperatorIDeleteTheCustomer()
    {
        var customerId = _scenarioContext.GetCustomers().Single().Id;

        var result = await _customerDriver.DeleteCustomerAsync(customerId);

        if (string.IsNullOrWhiteSpace(result.ErrorMessage) is false)
        {
            _scenarioContext.AddCustomerErrorMessage(result.ErrorMessage);
        }
    }


    [Then(@"the customer should be created successfully")]
    public async Task ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        var customerId = _scenarioContext.GetCustomers().Single().Id;
        await _customerDriver.AssertCustomerCreatedSuccessfullyAsync(customerId);
    }

    [Then(@"an error ""(.*)"" should be thrown")]
    public void ThenAnErrorShouldBeThrown(string errorMessage)
    {
        _customerDriver.AssertErrorMessage(errorMessage);
    }

    [Then(@"the customer should be updated successfully")]
    public async Task ThenTheCustomerShouldBeUpdatedSuccessfully()
    {
        var customerId = _scenarioContext.GetCustomers().Single().Id;
        var command = _scenarioContext.GetUpdateCustomerCommand();
        await _customerDriver.AssertCustomerUpdatedSuccessfullyAsync(customerId, command);
    }

    [Then(@"the customer should be deleted successfully")]
    public async Task ThenTheCustomerShouldBeDeletedSuccessfully()
    {
        var customerId = _scenarioContext.GetCustomers().Single().Id;
        await _customerDriver.AssertCustomerDeletedSuccessfullyAsync(customerId);
    }
}