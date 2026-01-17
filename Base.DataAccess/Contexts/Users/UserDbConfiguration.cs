using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Contexts.Users;

public class UserDbConfiguration : IEntityTypeConfiguration<UserPersistence>
{
    public void Configure(EntityTypeBuilder<UserPersistence> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
    }
}
