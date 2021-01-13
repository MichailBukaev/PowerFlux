using PouwerFlux.Data.Entityes.Interfaces;

namespace PouwerFlux.Data.Entityes
{
  public class AlloyingElement : Entity
  {
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string PartialTransformationToFerroalloyEquation { get; set; }
    public string PartialTransformationToKernelEquation { get; set; }
    public string PartialTransformationToGasEquation { get; set; }
    public string PartialTransformationToSlagEquation { get; set; }
  }
}
