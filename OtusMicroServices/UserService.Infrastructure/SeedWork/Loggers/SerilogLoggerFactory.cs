using Microsoft.Extensions.Configuration;
using Serilog;

namespace UserService.Infrastructure.SeedWork.Loggers
{
    public static class SerilogLoggerFactory
    {
        public static ILogger CreateLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .CreateLogger();
        }
    }
}
