using Base.Domain.Common;
using CSharpFunctionalExtensions;

namespace Base.Domain.Entities;

public enum UserIdentityProvider
{
    Auth0 = 0,
}

public class UserIdentity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public UserIdentityProvider Provider { get; private set; }

    public string ExternalUserId { get; private set; }
    public Guid UserId { get; private set; }

    private UserIdentity()
    {
    }

    private UserIdentity(UserIdentityProvider provider,
        string externalUserId)
    {
        Provider = provider;
        ExternalUserId = externalUserId;
    }

    public static Result<UserIdentity, ErrorCode> CreateFromAuth0(
        string externalUserId
    )
    {
        if (String.IsNullOrWhiteSpace(externalUserId))
            return Result.Failure<UserIdentity, ErrorCode>(ErrorCode.InvalidExternalUserId);
        return new UserIdentity(provider: UserIdentityProvider.Auth0, externalUserId: externalUserId);
    }

    public static UserIdentity Rehydrate(Guid id, UserIdentityProvider provider, string externalUserId, Guid userId)
    {
        return new UserIdentity()
        {
            Id = id,
            Provider = provider,
            ExternalUserId = externalUserId,
            UserId = userId
        };
    }
}
