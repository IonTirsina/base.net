using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Contexts.Users;

public class UserPersistence
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    public UserIdentityPersistence Identity { get; private set; }

    private UserPersistence() {} // EF Core requires parameterless constructor
    
    public UserPersistence(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email.Value;
        Identity = new UserIdentityPersistence(user.Identity);
    }

    public User ToDomain()
    {
        return User.Rehydrate(
            id: Id,
            name: Name,
            email: Email,
            identity: Identity.ToDomain()
        );
    }
}

internal class UserDbConfiguration : IEntityTypeConfiguration<UserPersistence>
{
    public void Configure(EntityTypeBuilder<UserPersistence> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        
        builder.HasIndex(u => u.Email).IsUnique();


        builder.HasOne(u => u.Identity)
            .WithOne()
            .HasForeignKey<UserIdentityPersistence>(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

