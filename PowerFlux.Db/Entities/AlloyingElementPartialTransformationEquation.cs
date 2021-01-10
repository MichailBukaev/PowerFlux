using PowerFlux.Db.Entities.Interfaces;

namespace PowerFlux.Db.Entities
{
  public class AlloyingElementPartialTransformationEquation : Entity
  {
    public int AlloyingElementId { get; set; }
    public AlloyingElement AlloyingElement { get; set; }
    public string ToFerroalloyEquation { get; set; }
    public string ToKernelEquation { get; set; }
    public string ToGasEquation { get; set; }
    public string ToSlagEquation { get; set; }
  }
}
