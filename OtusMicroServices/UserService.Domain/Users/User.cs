using UserService.Domain.SeedWork;

namespace UserService.Domain.Users;

public sealed class User : AggregateRoot<User, UserKey>
{
    public User(UserKey id)
    {
        Id = id;
    }
    public User(string userName, string firstName, string lastName, string email, string phone)
    {
        Id = new UserKey(Guid.NewGuid());
        SetUsername(userName);
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetPhone(phone);
    }
    #region Properties

    public UserKey Id { get; private set; }
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    #endregion

    #region AccessMethods

    public override UserKey GetKey() => Id;

    public User SetUsername(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentNullException(nameof(userName));
        }

        UserName = userName;
        return this;
    }

    public User SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentNullException(nameof(firstName));
        }

        FirstName = firstName;
        return this;
    }

    public User SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentNullException(nameof(lastName));
        }

        LastName = lastName;
        return this;
    }

    public User SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        Email = email;
        return this;
    }

    public User SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentNullException(nameof(phone));
        }

        Phone = phone;
        return this;
    }

    #endregion
}