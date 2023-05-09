using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure.SeedWork
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAutoMigrations(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            var useAutoMigrate = configuration.GetSection("DeployOptions:UseAutoMigrate")?.Get<bool>() ?? false;
            if (!useAutoMigrate) return applicationBuilder;

            using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            foreach (var databaseSchemeUpdater in scope.ServiceProvider.GetServices<IDatabaseSchemeUpdater>())
            {
                databaseSchemeUpdater.ApplyMigrations();
            }

            return applicationBuilder;
        }
    }
}