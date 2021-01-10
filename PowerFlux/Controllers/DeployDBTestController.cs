using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerFlux.Db.Deploy;

namespace PowerFlux.Controllers
{
  [Route("db-test")]
  [ApiController]
  public class DeployDBTestController : ControllerBase
  {
    private readonly IDeployService _deployService;

    public DeployDBTestController(IDeployService deployService)
    {
      _deployService = deployService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _deployService.Load();
      return Ok(result);
    }
  }
}
