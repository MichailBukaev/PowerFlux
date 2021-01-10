using PowerFlux.Services.Services.Computation.Models;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Collections.Generic;

namespace PowerFlux.Services.Services.FluxCalculator.Building.Product
{
  public class FluxMaterialBalanceParameters : IMaterialBalanceParameters
  {
    public double WeldingCurrent { get; set; }
    public double ArcVoltage { get; set; }
    public double ElectrodeDiameter { get; set; }
    public Dictionary<int, double> AlloyingElementIdPercentInMetalDepositedDictionary { get; set; }
    public double CoatingMassCoefficient { get; set; }
    public Dictionary<int, double> AlloyingElementIdPercentInWireDictionary { get; set; }
    public double SourcePower { get; set; }
    public double CrossSectionalArea { get; set; }
    public double MetalDepositedMass { get; set; }
    public Dictionary<int, double> AlloyingElementIdMassInMetalDepositedDictionary { get; set; }
    public double MetalLossCoefficient { get; set; }
    public List<AlloyingElementPartialTransformationCoefficients> AlloyingElementPartialTransformationCoefficients { get; set; }
    public double Alpha { get; set; }
    public double Beta { get; set; }
    public Dictionary<string, DimensionDescription> VariableEquationParametersDictionary => GetDictioanary();
    public EquationParameters EquationParameters => new EquationParameters(SourcePower, CrossSectionalArea);

    public void SetInputParameters(FluxParametersRequest input)
    {
      WeldingCurrent = input.WeldingCurrent;
      ArcVoltage = input.ArcVoltage;
      ElectrodeDiameter = input.ElectrodeDiameter;
      AlloyingElementIdPercentInMetalDepositedDictionary = input.AlloyingElementIdPercentInMetalDepositedDictionary;
      CoatingMassCoefficient = input.CoatingMassCoefficient;
    }

    private Dictionary<string, DimensionDescription> GetDictioanary()
    {
      return new Dictionary<string, DimensionDescription>
      {
        [nameof(SourcePower)] = new DimensionDescription(nameof(EquationParameters.P), "Full power of the heating source"),
        [nameof(CrossSectionalArea)] = new DimensionDescription(nameof(EquationParameters.S), "Cross-sectional area of the metal welding rodelectrode"),
      };
    }
  }
}
