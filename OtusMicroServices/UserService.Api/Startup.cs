using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Prometheus;
using Serilog;
using UserService.Api.Middleware;
using UserService.Infrastructure;
using UserService.Infrastructure.SeedWork;
using UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors;
using UserService.Infrastructure.SeedWork.JsonSerializers;
using UserService.Infrastructure.SeedWork.Loggers;

namespace UserService.Api;

public class Startup
{
    private static readonly IEnumerable<Assembly> AssembliesForScan = new[]
    {
        "UserService.Application"
    }.Select(Assembly.Load);

    public static WebApplication PrepareWebApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = AppConfigurationBuilder.Build();

        RegisterSerilog(builder);

        ConfigureServices(builder.Services, configuration);

        var webApplication = builder.Build();

        ConfigureApplication(webApplication, configuration);

        return webApplication;
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers(options =>
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

        var assembliesForScan = AssembliesForScan.ToArray();

        services.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssemblies(assembliesForScan);
        });

        services.AddJsonSerializer();
        services.AddUserServiceDatabase(configuration);
        services.AddExceptionDescriptors();
    }

    private static void ConfigureApplication(WebApplication app, IConfiguration configuration)
    {
        // if (app.Environment.IsDevelopment())
        //   {
        app.UseSwagger();
        app.UseSwaggerUI();
        //   }
        app.UseMetricServer();
        app.UseHttpMetrics();
        app.UseExceptionMiddleware();


        app
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });


        app.UseAutoMigrations(configuration);
    }

    private static void RegisterSerilog(WebApplicationBuilder builder)
    {
        var logger = SerilogLoggerFactory.CreateLogger(builder.Configuration);
        builder.Logging.AddSerilog(logger);
    }
}