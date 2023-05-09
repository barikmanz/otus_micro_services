namespace UserService.Infrastructure.SeedWork
{
    public interface IDatabaseSchemeUpdater
    {
        Task ApplyMigrationsAsync(CancellationToken cancellationToken);
        void ApplyMigrations();
    }
}