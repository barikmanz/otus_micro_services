namespace UserService.Infrastructure.SeedWork.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        Task UseTransaction(Func<Task> action, CancellationToken cancellationToken);
    }
}