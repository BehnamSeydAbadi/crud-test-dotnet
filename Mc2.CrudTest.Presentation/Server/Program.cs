using Mc2.CrudTest.Application;
using Mc2.CrudTest.Infrastructure;
using Mc2.CrudTest.Presentation.Server.Endpoints;

namespace Mc2.CrudTest.Presentation.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        PresentationBootstrapper.Run(builder.Services);
        ApplicationBootstrapper.Run(builder.Services);
        InfrastructureBootstrapper.Run(builder.Services, builder.Configuration);

        var app = builder.Build();

        CustomerEndpoints.Map(app);

        PresentationBootstrapper.SetUpMiddlewares(app);

        app.Run();
    }
}