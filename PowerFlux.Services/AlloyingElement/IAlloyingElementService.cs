using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerFlux.Services.AlloyingElement
{
  public interface IAlloyingElementService
  {
    public Task<IEnumerable<PouwerFlux.Context.Models.AlloyingElement>> GetAlloyingElements();
    public Task<PouwerFlux.Context.Models.AlloyingElement> GetAlloyingElement(int id);
    public Task UpdateAlloyingElement(PouwerFlux.Context.Models.AlloyingElement element);
  }
}
