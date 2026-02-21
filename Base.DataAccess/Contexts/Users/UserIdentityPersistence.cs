using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Contexts.Users;

public class UserIdentityPersistence
{
    public Guid Id { get; private set; }
    public UserIdentityProvider Provider { get; private set; }

    public string ExternalUserId { get; private set; }
    public Guid UserId { get; private set; }

    /// <summary>
    /// For EF Core
    /// </summary>
    private UserIdentityPersistence() {}
    
    public UserIdentityPersistence(UserIdentity identity)
    {
        Id = identity.Id;
        Provider = identity.Provider;
        ExternalUserId = identity.ExternalUserId;
        UserId = identity.UserId;
    }

    public UserIdentity ToDomain()
    {
        return UserIdentity.Rehydrate(
            id: Id,
            provider: Provider,
            externalUserId: ExternalUserId,
            userId: UserId
        );
    }
}

internal class UserIdentityDbConfiguration: IEntityTypeConfiguration<UserIdentityPersistence>
{
    public void Configure(EntityTypeBuilder<UserIdentityPersistence> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalUserId).IsRequired();
        builder.Property(x => x.Provider).HasConversion<string>().IsRequired();
        builder.Property(x => x.UserId).IsRequired(); 
    }
}
