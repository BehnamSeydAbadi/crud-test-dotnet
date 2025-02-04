using Mc2.CrudTest.Application.Customer.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;

internal static class ScenarioContextExtensions
{
    public static void AddMc2CrudTestPresentationServerHttpClient(
        this ScenarioContext scenarioContext, HttpClient httpClient
    )
    {
        scenarioContext.Set(httpClient, "mc2CrudTestPresentationServerHttpClient");
    }

    public static HttpClient GetMc2CrudTestPresentationServerHttpClient(this ScenarioContext scenarioContext)
    {
        return scenarioContext.Get<HttpClient>("mc2CrudTestPresentationServerHttpClient");
    }


    public static void AddMc2CrudTestPresentationServerServiceScope(
        this ScenarioContext scenarioContext, IServiceScope serviceScope
    )
    {
        scenarioContext.Set(serviceScope, "mc2CrudTestPresentationServerServiceScope");
    }

    public static IServiceScope GetMc2CrudTestPresentationServerServiceScope(this ScenarioContext scenarioContext)
    {
        return scenarioContext.Get<IServiceScope>("mc2CrudTestPresentationServerServiceScope");
    }


    public static void AddCustomerId(this ScenarioContext scenarioContext, Guid customerId)
    {
        scenarioContext.Set(customerId, "mc2CrudTestPresentationServerCustomerId");
    }

    public static Guid GetCustomerId(this ScenarioContext scenarioContext)
    {
        return scenarioContext.Get<Guid>("mc2CrudTestPresentationServerCustomerId");
    }


    public static void AddCreateCustomerCommand(this ScenarioContext scenarioContext, CreateCustomerCommand command)
    {
        scenarioContext.Set(command, "mc2CrudTestPresentationServerCreateCustomerCommand");
    }

    public static CreateCustomerCommand GetCreateCustomerCommand(this ScenarioContext scenarioContext)
    {
        return scenarioContext.Get<CreateCustomerCommand>("mc2CrudTestPresentationServerCreateCustomerCommand");
    }


    public static void AddCustomerErrorMessage(this ScenarioContext scenarioContext, string errorMessage)
    {
        scenarioContext.Set(errorMessage, "mc2CrudTestPresentationServerCustomerErrorMessage");
    }

    public static string GetCustomerErrorMessage(this ScenarioContext scenarioContext)
    {
        return scenarioContext.Get<string>("mc2CrudTestPresentationServerCustomerErrorMessage");
    }
}