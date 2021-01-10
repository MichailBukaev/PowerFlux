using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.FluxCalculator
{
  public interface IFluxCalculatorService
  {
    Task<FluxParametersResponse> CalculateStructureFluxAsync(FluxParametersRequest request);
  }
}
