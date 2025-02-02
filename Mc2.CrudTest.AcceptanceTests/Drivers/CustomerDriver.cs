namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerDriver
{
    private readonly ScenarioContext _scenarioContext;

    public CustomerDriver(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    public async Task<Guid> CreateCustomerAsync()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task AssertCustomerCreationAsync(Guid customerId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}