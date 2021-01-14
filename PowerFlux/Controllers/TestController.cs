using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerFlux.Db.Repositories.Interfaces;

namespace PowerFlux.Controllers
{
  [Route("test")]
  [ApiController]
  public class TestController : ControllerBase
  {
    private readonly ISettingsRepository _repository;

    public TestController(ISettingsRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string key)
    {
      var result = await _repository.GetSettingAsync(key);
      return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, string newValue)
    {
      var result =await _repository.UpdateSettingAsync(id, newValue);
      return Ok(result);
    }
  }
}
