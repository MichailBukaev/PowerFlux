using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PowerFlux.Db.DbContexts
{
	public class MigrationContextFactory : IDesignTimeDbContextFactory<PowerFluxContext>
	{
		public PowerFluxContext CreateDbContext(string[] args)
		{
      var optionsBuilder = new DbContextOptionsBuilder<PowerFluxContext>();

      ConfigurationBuilder builder = new ConfigurationBuilder();
      builder.SetBasePath(Directory.GetCurrentDirectory());
      builder.AddJsonFile("appsettings.json");
      IConfigurationRoot config = builder.Build();

      string connectionString = config.GetConnectionString("Database");
      optionsBuilder.UseSqlServer(connectionString);
      return new PowerFluxContext(optionsBuilder.Options);
    }
	}
}
