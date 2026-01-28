using Base.Application.Repositories.Users;

namespace Base.Application.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct);
    
    IUserWriteRepository UserWriteRepository { get; }
}
