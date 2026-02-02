using Base.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Base.Application.Repositories.Users;

public interface IUserWriteRepository
{
    public Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<Maybe<User>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    public Task AddAsync(User user, CancellationToken cancellationToken);
    public Task UpdateAsync(User user, CancellationToken cancellationToken);
}
