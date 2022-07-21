using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Persistence.Context;

public class KidsRUsContextFactory : IDesignTimeDbContextFactory<KidsRUsContext>
{
    public KidsRUsContext CreateDbContext(string[] args)
    {
        // Get environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Build config
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../KidsRUs.Api"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var dbPath = configuration["KidsRUsDb"];
        
        var optionsBuilder = new DbContextOptionsBuilder<KidsRUsContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}", b => b.MigrationsAssembly(typeof(KidsRUsContext).Assembly.FullName));
        return new KidsRUsContext(optionsBuilder.Options);
    }
}