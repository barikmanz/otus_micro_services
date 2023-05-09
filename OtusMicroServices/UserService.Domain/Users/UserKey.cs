using UserService.Domain.SeedWork;

namespace UserService.Domain.Users;

public sealed class UserKey : SimpleKey<UserKey>
{
    public UserKey(Guid value) : base(value)
    {
    }
}