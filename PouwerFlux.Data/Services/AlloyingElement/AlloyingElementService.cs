using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PouwerFlux.Data.Services.AlloyingElement
{
  public class AlloyingElementService : IAlloyingElementService
  {
    private readonly DbContext _dbContext;

    public AlloyingElementService(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<Entityes.AlloyingElement>> GetAlloyingElements() => await AlloyingElements().ToListAsync();

    public async Task<Entityes.AlloyingElement> GetAlloyingElement(int id)
    {
      var  result = await AlloyingElements().FirstOrDefaultAsync(a => a.Id.Equals(id));

      if (result == null)
        throw new ObjectNotFoundException($"AlloyingElement with id {id} not found");
      return result;
    }

    public async Task<IEnumerable<Entityes.AlloyingElement>> GetAlloyingElement(Expression<Func<Entityes.AlloyingElement, bool>> wereExpression)
      => await AlloyingElements().Where(wereExpression).ToListAsync();

    private DbSet<Entityes.AlloyingElement> AlloyingElements() => _dbContext.Set<Entityes.AlloyingElement>();
  }
}