using Mc2.CrudTest.Application;
using Mc2.CrudTest.Domain;
using Mc2.CrudTest.Infrastructure;
using Mc2.CrudTest.Infrastructure.ReadSide;
using Mc2.CrudTest.Presentation.Server.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DomainBootstrapper.Run(builder.Services);
        ApplicationBootstrapper.Run(builder.Services);
        InfrastructureBootstrapper.Run(builder.Services, builder.Configuration);
        PresentationBootstrapper.Run(builder.Services);

        var app = builder.Build();

        EnsureReadSideDatabaseCreated(app);

        CustomerEndpoints.Map(app);

        PresentationBootstrapper.SetUpMiddlewares(app);

        app.Run();
    }

    private static void EnsureReadSideDatabaseCreated(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<Mc2CrudTestDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
        else
        {
            dbContext.Database.EnsureCreated();
        }
    }
}