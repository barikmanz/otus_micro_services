using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using UserService.Infrastructure;
using UserService.Infrastructure.SeedWork;
using UserService.Infrastructure.SeedWork.Loggers;

using (var host = CreateHostBuilder(args).Build())
{
    Console.WriteLine("Running migrations...");
    Thread.Sleep(30_000);

    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
    var updater = host.Services.GetService<IDatabaseSchemeUpdater>();
    if (updater == null)
    {
        throw new NullReferenceException("DbUpdater is null");
    }

    await updater.ApplyMigrationsAsync(default);

    lifetime.StopApplication();
    await host.WaitForShutdownAsync();
    Console.WriteLine("Migrations complete!");
}


static IHostBuilder CreateHostBuilder(string[] args)
{
    var configuration = AppConfigurationBuilder.Build();
    var host = Host
        .CreateDefaultBuilder(args)
        .UseConsoleLifetime()
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog(SerilogLoggerFactory.CreateLogger(configuration));
        })
        .ConfigureServices((hostContext, services) => ConfigureServices(services, configuration));

    return host;
}

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddUserServiceDatabase(configuration);
}