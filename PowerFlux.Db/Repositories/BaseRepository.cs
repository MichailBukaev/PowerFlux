using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerFlux.Db.DbContexts;
using PowerFlux.Db.ModelsDb.Interfaces;

namespace PowerFlux.Db.Repositories
{
  public abstract class BaseRepository<TEntity> where TEntity : DbEntity
  {
    private readonly DbContextOptions<PowerFluxContext> _dbContextOptions;
    protected BaseRepository(DbContextOptions<PowerFluxContext> dbContextOptions)
    {
      _dbContextOptions = dbContextOptions;
    }

    protected async Task<IEnumerable<TEntity>> GetDbEntitiesAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate)
    {
      IEnumerable<TEntity> result;
      using (var context = new PowerFluxContext(_dbContextOptions))
      {
        result = await predicate(context.Set<TEntity>()).ToListAsync();
      }

      return result;
    }

    protected async Task<TEntity> GetDbEntityAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate)
    {
      TEntity result;
      using (var context = new PowerFluxContext(_dbContextOptions))
      {
        result = await predicate(context.Set<TEntity>()).FirstOrDefaultAsync();
      }
      return result;
    }

    protected Task<TEntity> InsertEntityAsync(Func<DbSet<TEntity>, Task<TEntity>> func) => ChangeAsync(func);

    protected Task<IEnumerable<TEntity>> InsertEntitiesAsync(Func<DbSet<TEntity>, Task<IEnumerable<TEntity>>> func) => ChangeAsync(func);

    protected Task<TEntity> UpdateEntityAsync(Func<DbSet<TEntity>, Task<TEntity>> func) => ChangeAsync(func);

    protected Task<IEnumerable<TEntity>> UpdateEntitiesAsync(Func<DbSet<TEntity>, Task<IEnumerable<TEntity>>> func) => ChangeAsync(func);

    protected Task<TEntity> DeleteEntityAsync(Func<DbSet<TEntity>, Task<TEntity>> func) => ChangeAsync(func);

    protected Task<IEnumerable<TEntity>> DeleteEntitiesAsync(Func<DbSet<TEntity>, Task<IEnumerable<TEntity>>> func) => ChangeAsync(func);

    private async Task<T> ChangeAsync<T>(Func<DbSet<TEntity>, Task<T>> func)
    {
      T result;
      using (var context = new PowerFluxContext(_dbContextOptions))
      {
        var transaction = context.Database.BeginTransaction();
        try
        {
          result = await func(context.Set<TEntity>());
          await context.SaveChangesAsync();
          transaction.Commit();
        }
        catch (DbUpdateConcurrencyException e)
        {
          transaction.Rollback();
          throw new Exception("The object has already been changed before");
        }
        catch (Exception e)
        {
          transaction.Rollback();
          throw e;
        }
      }
      return result;
    }
  }
}
