using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer;
using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Hooks;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BuildMc2CrudTestPresentationServerWebApplication()
    {
        var webApplicationFactory = new Mc2CrudTestPresentationServerWebApplicationFactory();

        _scenarioContext.AddMc2CrudTestPresentationServerHttpClient(webApplicationFactory.CreateClient());

        _scenarioContext.AddMc2CrudTestPresentationServerServiceScope(webApplicationFactory.Services.CreateScope());
    }
}