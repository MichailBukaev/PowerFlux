using PowerFlux.Services.Services.FluxCalculator.Building.Builder;
using PowerFlux.Services.Services.FluxCalculator.Building.Product;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.FluxCalculator.Building.Director
{
  public interface IMaterialBalanceParametersDirector
  {
    public Task<IMaterialBalanceParameters> CreateParameters(IMaterialBalanceParametersBuilder builder, FluxParametersRequest input);
  }
}
