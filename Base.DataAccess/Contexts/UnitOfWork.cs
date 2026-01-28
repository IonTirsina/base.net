using Base.Application.Repositories;
using Base.Application.Repositories.Users;

namespace Base.DataAccess.Contexts;

/// <summary>
/// EF Core Unit Of Work, name simplified
/// </summary>
public class UnitOfWork(BaseDbContext dbContext, IUserWriteRepository userWriteRepository)
    : IUnitOfWork
{
    public IUserWriteRepository UserWriteRepository => userWriteRepository;

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await dbContext.SaveChangesAsync(ct);
    }
}
