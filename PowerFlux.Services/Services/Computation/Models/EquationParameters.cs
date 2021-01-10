namespace PowerFlux.Services.Services.Computation.Models
{
  public class EquationParameters
  {
    public double P { get; set; }
    public double S { get; set; }
    public EquationParameters(double sourcePower, double electrodeCrossSectionalArea)
    {
      P = sourcePower;
      S = electrodeCrossSectionalArea;
    }
  }
}
