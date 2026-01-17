using Base.Domain.ValueObjects;

namespace Base.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public Email Email { get; private set; }
    
    private User(string name, Email email)
    {
        Name = name;
        Email = email;
    }

    public static User Create(
        string name,
        Email email)
    {
        if (String.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name cannot be empty");

        if (Email.IsValid(email.Value) is false)
        {
            throw new ArgumentException("Invalid user email");
        }
        
        return new User(name, email);
    }
    
    private User () {}
    
    // Regydrate from database
    public static User Rehydrate(Guid id, string name, string email)
    {
        return new User
        {
            Id = id,
            Name = name,
            Email = Email.Create(email)
        };
    }
}
