using PowerFlux.Services.Services.Computation.Models;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Collections.Generic;

namespace PowerFlux.Services.Services.FluxCalculator.Building.Product
{
  public interface IMaterialBalanceParameters
  {
    Dictionary<int, double> AlloyingElementIdMassInMetalDepositedDictionary { get; set; }
    Dictionary<int, double> AlloyingElementIdPercentInMetalDepositedDictionary { get; set; }
    Dictionary<int, double> AlloyingElementIdPercentInWireDictionary { get; set; }
    List<AlloyingElementPartialTransformationCoefficients> AlloyingElementPartialTransformationCoefficients { get; set; }
    double ArcVoltage { get; set; }
    double CoatingMassCoefficient { get; set; }
    double CrossSectionalArea { get; set; }
    double ElectrodeDiameter { get; set; }
    double MetalDepositedMass { get; set; }
    double MetalLossCoefficient { get; set; }
    double SourcePower { get; set; }
    double WeldingCurrent { get; set; }
    double Alpha { get; set; }
    double Beta { get; set; }
    void SetInputParameters(FluxParametersRequest input);
    Dictionary<string, DimensionDescription> VariableEquationParametersDictionary { get; }
    EquationParameters EquationParameters { get; }
  }
}