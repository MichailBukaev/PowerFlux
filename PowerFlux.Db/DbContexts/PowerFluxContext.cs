using Microsoft.EntityFrameworkCore;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.DbContexts
{
  public class PowerFluxContext : DbContext
  {
    public DbSet<DbSetting> Settings { get; set; }
    public DbSet<DbAlloyingElement> AlloyingElements { get; set; }

    internal PowerFluxContext(DbContextOptions<PowerFluxContext> options)
      :base(options)
    {
    }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Seed();
		}
	}
}
