using Base.Domain.Common;
using Base.Domain.ValueObjects;
using CSharpFunctionalExtensions;

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

    public static Result<User, ErrorCode> Create(
        string name,
        Email email,
        string password)
    {
        // TODO : move to validator
        // if (String.IsNullOrWhiteSpace(name))
        //     return Result.Failure<User>("User name cannot be empty");
        //
        // if (Email.IsValid(email.Value) is false)
        //     return Result.Failure<User>("Invalid user email");
        //
        // if (String.IsNullOrWhiteSpace(password))
        //     return Result.Failure<User>("Password cannot be empty");
        
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
