namespace Mc2.CrudTest.Presentation.Server;

internal class PresentationBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        serviceCollection.AddHttpClient();
        serviceCollection.AddControllersWithViews();
    }

    public static void SetUpMiddlewares(WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStaticFiles();

        app.UseRouting();
    }
}