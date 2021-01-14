using Microsoft.Extensions.Configuration;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.Repositories
{
  public class AlloyingElementsRepository : BaseRepository<DbAlloyingElement>
  {
    public AlloyingElementsRepository(IConfiguration configuration) : base(configuration)
    {
    }
  }
}
