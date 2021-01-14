using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.DbContexts
{
  internal class PowerFluxContext : DbContext
  {
    internal PowerFluxContext(IConfiguration configuration)
    : base(configuration.GetConnectionString("Database"))
    {
      Database.SetInitializer<PowerFluxContext>(new PowerFluxContextInitializer());
    }

    private DbSet<DbSetting> Settings { get; set; }
    private DbSet<DbAlloyingElement> AlloyingElements {get; set;}
  }
}
