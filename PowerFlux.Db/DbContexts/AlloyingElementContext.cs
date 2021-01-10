using PowerFlux.Db.DbContexts.EntityContextBase;
using PowerFlux.Db.Entities;
using PowerFlux.Db.DbContexts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PowerFlux.Db.DbContexts
{
  public class AlloyingElementContext : EntityContextBase<AlloyingElement>, IAlloyingElementContext
  {
    public AlloyingElementContext(IConfiguration configuration) : base(configuration)
    {
    }
  }
}
