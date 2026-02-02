using Base.Application.Repositories;
using Base.Application.Repositories.Users;
using Base.Domain.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Contexts.Users;

/// <summary>
/// A specialized repository focused exclusively on <b>Command/Write</b> operations for <see cref="User"/> aggregates.
/// </summary>
/// <remarks>
/// <para>
/// <b>Persistence Logic:</b> This repository encapsulates the logic for adding, modifying, and removing 
/// user entities within the underlying <see cref="BaseDbContext"/>. It follows the 
/// <b>Unit of Work</b> pattern by design.
/// </para>
/// <para>
/// <b>Transaction Management:</b> Crucially, this class is <b>stateless regarding transactions</b>. 
/// It does not call <c>SaveChanges()</c> or <c>SaveChangesAsync()</c>. This ensures that multiple 
/// repository operations across different aggregates can be bundled into a single atomic transaction 
/// orchestrated by the <see cref="IUnitOfWork"/>.
/// </para>
/// <example>
/// <code>
/// // Typical Usage Pattern:
/// await _userWriteRepo.AddAsync(newUser);
/// await _profileWriteRepo.AddAsync(newProfile);
/// // The Unit of Work ensures both (or neither) are persisted.
/// await _unitOfWork.CommitAsync(); 
/// </code>
/// </example>
/// </remarks>
public class UserWriteRepository(BaseDbContext dbContext) : IUserWriteRepository
{
    /// <summary>
    /// "Why here?" you may ask. Reads used for decision-making belong to write side
    /// </summary>
    public async Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        return user?.ToDomain();
    }

    public async Task<Maybe<User>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user?.ToDomain();
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(new UserPersistence(user), cancellationToken);
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        // EF Core tracks changes in memory, no real async needed
        dbContext.Users.Update(new UserPersistence(user));

        // "Fake async" to satisfy interface
        return Task.CompletedTask;
    }
}
