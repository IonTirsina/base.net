using Base.Domain.Entities;

namespace Base.Application.Repositories.Users;

public interface IUserWriteRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task AddAsync(User user, CancellationToken cancellationToken);
    public Task UpdateAsync(User user, CancellationToken cancellationToken);
}
