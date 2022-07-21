using KidsRUs.Application.Repositories.Common;
using KidsRUs.Persistence.Context;
using KidsRUs.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KidsRUs.Persistence;

public static class Startup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbPath = configuration["KidsRUsDb"];
        
        services.AddDbContext<KidsRUsContext>(options =>
            options.UseSqlite($"Data Source={dbPath}",
               b => b.MigrationsAssembly(typeof(KidsRUsContext).Assembly.FullName)));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        
        // Seed
        
        var container = services.BuildServiceProvider();
        var unitOfWork = container.GetRequiredService<IUnitOfWork>();

        DefaultUsers.SeedAsync(unitOfWork);

        return services;
    }
}