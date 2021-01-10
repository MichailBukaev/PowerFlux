using System.Collections.Generic;
using PowerFlux.Db.Entities.Interfaces;

namespace PowerFlux.Db.Entities
{
  public class AlloyingElement : Entity
  {
    public string Name { get; set; }
    public string Symbol { get; set; }
    public List<AlloyingElementPartialTransformationEquation> PartialTransformationEquations { get; set; }
  }
}
