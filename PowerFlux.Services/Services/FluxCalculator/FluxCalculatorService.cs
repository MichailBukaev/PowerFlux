using PowerFlux.Services.Services.FluxCalculator.Building.Builder.Flux;
using PowerFlux.Services.Services.FluxCalculator.Building.Director;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.FluxCalculator
{
  public class FluxCalculatorService : IFluxCalculatorService
  {
    private readonly IFluxMaterialBalanceParametersBuilder _builder;
    private readonly IMaterialBalanceParametersDirector _director;

    public FluxCalculatorService(
      IFluxMaterialBalanceParametersBuilder builder,
      IMaterialBalanceParametersDirector director)
    {
      _builder = builder;
      _director = director;
    }
    public async Task<FluxParametersResponse> CalculateStructureFluxAsync(FluxParametersRequest request)
    {
      var parameters = await _director.CreateParameters(_builder, request);
      throw new System.NotImplementedException();
    }
  }
}
