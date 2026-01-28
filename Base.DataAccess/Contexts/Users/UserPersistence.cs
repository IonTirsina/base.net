using Base.Domain.Entities;

namespace Base.DataAccess.Contexts.Users;

public record UserPersistence
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    private UserPersistence() {} // EF Core requires parameterless constructor
    
    public UserPersistence(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email.Value;
        PasswordHash = user.PasswordHash;
    }

    public User ToDomain()
    {
        return User.Rehydrate(
            id: Id,
            name: Name,
            email: Email,
            passwordHash: PasswordHash
        );
    }
}
