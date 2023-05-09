using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.SeedWork
{
    internal class DatabaseSchemeUpdater<TDbContext> : IDatabaseSchemeUpdater where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly ILogger<IDatabaseSchemeUpdater> _logger;

        public DatabaseSchemeUpdater(TDbContext dbContext, ILogger<IDatabaseSchemeUpdater> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task ApplyMigrationsAsync(CancellationToken cancellationToken)
        {
            var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToArray();
            if (pendingMigrations.Any())
            {
                _logger.LogWarning($"Applying migrations for {typeof(TDbContext).Name}...\n" +
                                   $"Migrations to apply:\n\t{string.Join("\n\t", pendingMigrations)}");
                await _dbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogWarning($"Migrations for {typeof(TDbContext).Name} applied successfully.");
                return;
            }

            _logger.LogWarning($"No migrations to apply for {typeof(TDbContext).Name}.");
        }

        public void ApplyMigrations()
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations().ToArray();
            if (pendingMigrations.Any())
            {
                _logger.LogWarning($"Applying migrations for {typeof(TDbContext).Name}...\n" +
                                   $"Migrations to apply:\n\t{string.Join("\n\t", pendingMigrations)}");
                _dbContext.Database.Migrate();
                _logger.LogWarning($"Migrations for {typeof(TDbContext).Name} applied successfully.");
                return;
            }

            _logger.LogWarning($"No migrations to apply for {typeof(TDbContext).Name}.");
        }
    }
}