using Microsoft.Extensions.Configuration;
using PowerFlux.Db.DbContexts.EntityContextBase;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Db.Entities;

namespace PowerFlux.Db.DbContexts
{
  public class AlloyingElementPartialTransformationEquationContext : EntityContextBase<AlloyingElementPartialTransformationEquation>, IAlloyingElementPartialTransformationEquationContext
  {
    public AlloyingElementPartialTransformationEquationContext(IConfiguration configuration) : base(configuration)
    {
    }
  }
}
