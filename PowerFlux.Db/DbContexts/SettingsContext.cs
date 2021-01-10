using Microsoft.Extensions.Configuration;
using PowerFlux.Db.DbContexts.EntityContextBase;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Db.Entities;

namespace PowerFlux.Db.DbContexts
{
  public class SettingsContext : EntityContextBase<Settings>, ISettingsContext
  {
    public SettingsContext(IConfiguration configuration) : base(configuration)
    {
    }
  }
}
