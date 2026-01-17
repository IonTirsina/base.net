using Base.Application.Repositories.Users;
using Base.DataAccess.Contexts;
using Base.DataAccess.Contexts.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Base.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection RegisterPostgresDatabase(this IServiceCollection services, string connectionString)
    {
        if (String.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be empty");

        services.AddDbContext<BaseDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        return services;
    }
}
