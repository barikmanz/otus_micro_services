using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;
using UserService.Domain.Users.Repository;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserServiceDbContext _userServiceDbContext;

    public UserRepository(UserServiceDbContext userServiceDbContext)
    {
        _userServiceDbContext = userServiceDbContext;
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var createdUser = await _userServiceDbContext.Users.AddAsync(user, cancellationToken);
        return createdUser.Entity;
    }

    public void Update(User user)
    {
        _userServiceDbContext.Update(user);
    }


    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userKey = new UserKey(id);
        var user = await _userServiceDbContext.Users.FirstOrDefaultAsync(u => u.Id == userKey, cancellationToken);
        return user;
    }

    public void Delete(Guid id)
    {
        var userKey = new UserKey(id);
        var user = new User(userKey);
        _userServiceDbContext.Attach(user);
        _userServiceDbContext.Users.Remove(user);
    }
}