using PowerFlux.Services.Services.FluxCalculator.Building.Product;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.FluxCalculator.Building.Builder
{
  public interface IMaterialBalanceParametersBuilder
  {
    void SetInputParameters(FluxParametersRequest input);
    void ComputAlloyingElementMassInMetalDeposited();
    Task ComputAlloyingElementPartialTransformationCoefficients();
    void ComputElectrodeCrossSectionalArea();
    void ComputMetalDepositedMass();
    void ComputMetalLossCoefficient();
    void ComputSourcePower();
    IMaterialBalanceParameters GetMaterialBalanceParameters();
  }
}