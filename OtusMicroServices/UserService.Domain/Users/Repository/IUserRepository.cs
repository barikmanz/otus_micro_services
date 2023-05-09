namespace UserService.Domain.Users.Repository;

public interface IUserRepository
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    void Update(User user);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Delete(Guid id);
}