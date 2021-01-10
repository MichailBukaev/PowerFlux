using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PowerFlux.Db.DbContexts.DataBase;

namespace PowerFlux.Db.DbContexts.EntityContextBase
{
  public abstract class EntityContextBase<TEntity> : IEntityContextBase<TEntity> where TEntity : class
  {
    protected readonly DbSet<TEntity> DbSet;
    protected readonly DbContext DbContext;

    public IQueryable<TEntity> Entities => DbSet;

    protected EntityContextBase(IConfiguration configuration)
    {
      DbContext = DataContext.GetContext(configuration);
      DbSet = DbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
      var result = DbSet.Add(entity);
      await DbContext.SaveChangesAsync();
      return result;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
      var result = DbSet.AddRange(entities);
      await DbContext.SaveChangesAsync();
      return result;
    }


    public virtual async Task UpdateAsync(TEntity entity)
    {
      DbContext.Entry(entity).State = EntityState.Modified;
      await DbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
      DbSet.Remove(entity);
      await DbContext.SaveChangesAsync();
    }

    public virtual DbContextTransaction GetAndBeginTransaction() => DbContext.Database.BeginTransaction();
  }
}