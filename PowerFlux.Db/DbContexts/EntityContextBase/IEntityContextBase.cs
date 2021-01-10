using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PowerFlux.Db.DbContexts.EntityContextBase
{
  public interface IEntityContextBase<TEntity> where TEntity : class
  {
    public IQueryable<TEntity> Entities { get; }
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    DbContextTransaction GetAndBeginTransaction();
  }
}
