using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;

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
        var customerId = await _customerDriver.CreateCustomerAsync();
        _scenarioContext.AddCustomerId(customerId);
    }

    [Then(@"the customer should be created successfully")]
    public async Task ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        var customerId = _scenarioContext.GetCustomerId();
        await _customerDriver.AssertCustomerCreationAsync(customerId);
    }
}