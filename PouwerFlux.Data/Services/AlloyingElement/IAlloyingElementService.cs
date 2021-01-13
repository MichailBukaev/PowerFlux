using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PouwerFlux.Data.Services.AlloyingElement
{
  public interface IAlloyingElementService
  {
    public Task<IEnumerable<Entityes.AlloyingElement>> GetAlloyingElements();
    public Task<Entityes.AlloyingElement> GetAlloyingElement(int id);
    public Task<IEnumerable<Entityes.AlloyingElement>> GetAlloyingElement(Expression<Func<Entityes.AlloyingElement, bool>> wereExpression);
    public SaveChages();
  }
}
