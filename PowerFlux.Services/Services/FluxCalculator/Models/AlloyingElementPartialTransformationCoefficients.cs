namespace PowerFlux.Services.Services.FluxCalculator.Models
{
  public class AlloyingElementPartialTransformationCoefficients
  {
    public int AlloyingElementId { get; private set; }

    public double ToFerroalloy { get; set; }
    public double ToKernel { get; set; }
    public double ToGas { get; set; }
    public double ToSlag { get; set; }

    public AlloyingElementPartialTransformationCoefficients(int alloyingElementId)
    {
      AlloyingElementId = alloyingElementId;
    }
  }
}
