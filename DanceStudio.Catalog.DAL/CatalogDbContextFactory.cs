using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DanceStudio.Catalog.DAL
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("CatalogConnection");

            var builder = new DbContextOptionsBuilder<CatalogDbContext>();
            builder.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName));

            return new CatalogDbContext(builder.Options);
        }
    }
}