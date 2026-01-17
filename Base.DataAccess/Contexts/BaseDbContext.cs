using Base.DataAccess.Contexts.Users;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Contexts;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    public DbSet<UserPersistence> Users => Set<UserPersistence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
    }
    
}
