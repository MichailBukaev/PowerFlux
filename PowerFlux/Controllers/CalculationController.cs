//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading.Tasks;

//namespace PowerFlux.Controllers
//{
//	[ApiController]
//  [Route("[controller]")]
//  public class CalculationController : ControllerBase
//  {
//    private readonly IFluxCalculatorService _fluxCalculatorService;
//    private readonly ILogger<CalculationController> _logger;
//    public CalculationController(IFluxCalculatorService fluxCalculatorService, ILogger<CalculationController> logger)
//    {
//      _fluxCalculatorService = fluxCalculatorService;
//      _logger = logger;
//    }

//    [HttpPost]
//    public async Task<IActionResult> CalculateStructure(FluxParametersRequest request)
//    {
//      try
//      {
//        var response = await _fluxCalculatorService.CalculateStructureFluxAsync(request);
//        return Ok(response);
//      }
//      catch (Exception ex)
//      {
//        _logger.LogError("Error when calculate structure for flux {@fluxParametersRequest}. Error {@exception}",
//          request, ex);
//        return StatusCode(500, ex.Message);
//      }
//    }
//  }
//}
