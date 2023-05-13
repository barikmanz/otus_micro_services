using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.SeedWork.UnitOfWork
{
    internal sealed class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        private readonly ILogger<UnitOfWork<TDbContext>> _logger;

        public UnitOfWork(TDbContext dbContext,
            ILogger<UnitOfWork<TDbContext>> logger)
        {
            _dbContext = dbContext;

            _logger = logger;
        }

        public async Task UseTransaction(Func<Task> action, CancellationToken cancellationToken)
        {
            await using var tran = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();

                await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CommitTransactionAsync completed with error.");

                await _dbContext.Database.RollbackTransactionAsync(cancellationToken);

                throw;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            if (!_dbContext.ChangeTracker.HasChanges())
                return 0;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}