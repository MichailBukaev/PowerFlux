namespace PowerFlux.Services.Services.FluxCalculator.Models
{
  public class DimensionDescription
  {
    public string Dimension { get; private set; }
    public string Desription { get; private set; }

    public DimensionDescription(string dimension, string desription)
    {
      Dimension = dimension;
      Desription = desription;
    }
  }
}
