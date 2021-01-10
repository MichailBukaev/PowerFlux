using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PowerFlux.Db.DbContexts.EntityContextBase;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Db.Entities;

namespace PowerFlux.Db.DbContexts
{
  public class DbVersionContext : EntityContextBase<DbVersion>, IDbVersionContext
  {
    public DbVersionContext(IConfiguration configuration)
      : base(configuration)
    {
    }

    public override Task<DbVersion> AddAsync(DbVersion entity)
    {
      if(DbSet.Any())
        throw new ArgumentException("DbVersions already exists.");

      return base.AddAsync(entity);
    }

    public override Task DeleteAsync(DbVersion entity)
    {
      throw new InvalidOperationException("DbVersions cannot be deleted");
    }
  }
}
