using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PowerFlux.Services.Services.FluxCalculator.Models
{
  public class FluxParametersRequest
  {
    [Required]
    public double WeldingCurrent { get; set; }
    [Required]
    public double ArcVoltage { get; set; }
    [Required]
    public double ElectrodeDiameter { get; set; }
    [Required]
    public Dictionary<int, double> AlloyingElementIdPercentInMetalDepositedDictionary { get; set; }
    [Required]
    public double CoatingMassCoefficient { get; set; }
  }
}
