using Base.Domain.Common;
using Base.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Base.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public string Name { get; private set; }
    public Email Email { get; private set; }
    
    public UserIdentity Identity { get; private set; }
    
    private User(string name, Email email, UserIdentity identity)
    {
        Name = name;
        Email = email;
        Identity = identity;
    }

    public static Result<User, ErrorCode> Create(
        string name,
        Email email,
        UserIdentity identity
        )
    {
        
        return new User(name, email, identity);
    }
    
    private User () {}
    
    /// <summary>
    /// Rehydrate from database to domain
    /// </summary>
    /// <returns></returns>
    public static User Rehydrate(Guid id, string name, string email, UserIdentity identity)
    {
        return new User
        {
            Id = id,
            Name = name,
            Email = Email.FromTrustedSource(email),
            Identity = identity
        };
    }
}
