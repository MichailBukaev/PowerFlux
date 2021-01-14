using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Threading.Tasks;

namespace PowerFlux.Services.AlloyingElement
{
  public class AlloyingElementService : IAlloyingElementService
  {
    private readonly DbContext _dbContext;

    public AlloyingElementService(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<PouwerFlux.Context.Models.AlloyingElement>> GetAlloyingElements()=> await AlloyingElements().ToListAsync();

    public async Task<PouwerFlux.Context.Models.AlloyingElement> GetAlloyingElement(int id)
    {
      var element = await AlloyingElements().FirstOrDefaultAsync(a => a.Id.Equals(id));

      if (element == null)
        throw new ObjectNotFoundException($"AlloyingElement with id {id} not found");
      return element;
    }

    public Task UpdateAlloyingElement(PouwerFlux.Context.Models.AlloyingElement element)
    {
      _dbContext.Entry(element).State = EntityState.Modified;
      return _dbContext.SaveChangesAsync();
    }

    private DbSet<PouwerFlux.Context.Models.AlloyingElement> AlloyingElements() => _dbContext.Set<PouwerFlux.Context.Models.AlloyingElement>();
  }
}