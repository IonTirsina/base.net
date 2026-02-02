using Microsoft.Extensions.DependencyInjection;

namespace Base.Application;

public static class DependencyInjection
{
    public static void ConfigureApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = ""; // TODO : register
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
    }
}
