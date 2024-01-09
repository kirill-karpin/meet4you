using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Install;

public static class EntityFrameworkInstaller
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(
                connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly("WebApp"))
        );

        return services;
    }
}