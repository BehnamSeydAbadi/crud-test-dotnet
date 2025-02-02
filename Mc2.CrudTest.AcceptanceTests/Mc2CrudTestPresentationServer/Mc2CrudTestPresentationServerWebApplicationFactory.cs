using Mc2.CrudTest.Presentation.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer;

public class Mc2CrudTestPresentationServerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}