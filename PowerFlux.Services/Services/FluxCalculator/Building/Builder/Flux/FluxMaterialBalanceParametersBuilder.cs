using PowerFlux.Services.Services.FluxCalculator.Building.Product;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.FluxCalculator.Building.Builder.Flux
{
  public class FluxMaterialBalanceParametersBuilder : IFluxMaterialBalanceParametersBuilder
  {
    private readonly IMaterialBalanceParameters _product;

    public FluxMaterialBalanceParametersBuilder()
    {
      _product = new FluxMaterialBalanceParameters();
    }
    public void ComputAlloyingElementMassInMetalDeposited()
    {
      throw new NotImplementedException();
    }

    public Task ComputAlloyingElementPartialTransformationCoefficients()
    {
      throw new NotImplementedException();
    }

    public void ComputElectrodeCrossSectionalArea()
    {
      throw new NotImplementedException();
    }

    public void ComputMetalDepositedMass()
    {
      throw new NotImplementedException();
    }

    public void ComputMetalLossCoefficient()
    {
      throw new NotImplementedException();
    }

    public void ComputSourcePower()
    {
      throw new NotImplementedException();
    }

    public IMaterialBalanceParameters GetMaterialBalanceParameters() => _product;

    public void SetInputParameters(FluxParametersRequest input) => _product.SetInputParameters(input);
  }
}
