using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.Infra.Repository.Context;

public class AlmoxarifadoContextFactory : IDesignTimeDbContextFactory<HealthMedContext>
{
    public HealthMedContext CreateDbContext(string[] args)
    {
        var configPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(configPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HealthMedContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("HealthMedConnection"));

        return new HealthMedContext(optionsBuilder.Options);
    }
}
