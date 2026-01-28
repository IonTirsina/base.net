using Base.Domain.ValueObjects;

namespace Base.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    
    private User(string name, Email email,  string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static User Create(
        string name,
        Email email,
        string password)
    {
        if (String.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name cannot be empty");

        if (Email.IsValid(email.Value) is false)
        {
            throw new ArgumentException("Invalid user email");
        }
        
        if (String.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty");
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, 12);
        
        return new User(name, email, passwordHash);
    }
    
    private User () {}
    
    /// <summary>
    /// Rehydrate from database to domain
    /// </summary>
    /// <returns></returns>
    public static User Rehydrate(Guid id, string name, string email, string passwordHash)
    {
        return new User
        {
            Id = id,
            Name = name,
            Email = Email.Create(email),
            PasswordHash =  passwordHash
        };
    }
}
