using Microsoft.EntityFrameworkCore;
using PowerFlux.Db.DbContexts;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.Repositories
{
  public class AlloyingElementsRepository : BaseRepository<DbAlloyingElement>
  {
    public AlloyingElementsRepository(DbContextOptions<PowerFluxContext> dbContextOptions) : base(dbContextOptions)
    {
    }
  }
}
