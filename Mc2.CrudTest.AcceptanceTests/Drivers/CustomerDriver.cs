using System.Net.Http.Json;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;
using Mc2.CrudTest.Application.Customer.Command;
using Mc2.CrudTest.Application.Customer.Query.ViewModels;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerDriver
{
    private readonly ScenarioContext _scenarioContext;

    public CustomerDriver(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    public async Task<Guid> CreateCustomerAsync(CreateCustomerCommand command)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.PostAsync("/api/customers", JsonContent.Create(command));

        return await httpResponseMessage.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<CustomerViewModel?> GetCustomerAsync(Guid id)
    {
        var httpClient = _scenarioContext.GetMc2CrudTestPresentationServerHttpClient();

        var httpResponseMessage = await httpClient.GetAsync($"/api/customers/{id}");

        return await httpResponseMessage.Content.ReadFromJsonAsync<CustomerViewModel>();
    }
}