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


    [When(@"As a an operator, I create the customer with the following details:")]
    public async Task WhenAsAAnOperatorICreateTheCustomerWithTheFollowingDetails(Table table)
    {
        var command = table.GetCreateCustomerCommand();
        _scenarioContext.AddCreateCustomerCommand(command);

        var customerId = await _customerDriver.CreateCustomerAsync(command);
        _scenarioContext.AddCustomerId(customerId);
    }

    [Then(@"the customer should be created successfully")]
    public async Task ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        var serviceScope = _scenarioContext.GetMc2CrudTestPresentationServerServiceScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        var customerReadModel = await dbContext.Set<CustomerReadModel>().SingleOrDefaultAsync();

        customerReadModel.Should().NotBeNull();

        var command = _scenarioContext.GetCreateCustomerCommand();

        customerReadModel!.FirstName.Should().Be(command.FirstName);
        customerReadModel.LastName.Should().Be(command.LastName);
        customerReadModel.DateOfBirth.Should().Be(command.DateOfBirth);
        customerReadModel.PhoneNumber.Should().Be(command.PhoneNumber);
        customerReadModel.Email.Should().Be(command.Email);
        customerReadModel.BankAccountNumber.Should().Be(command.BankAccountNumber);
    }
}