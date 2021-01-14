using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.Setting
{
  public class SettingService
  {
    private readonly DbContext _dbContext;

    public SettingService(DbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task<T> GetValueAs<T>(string key, Settings.TryParseHandler<T> predicat) where T : struct
    {
      //var setting = await _dbContext.Entities.FirstOrDefaultAsync(s => s.Key.Equals(key));
      //if (setting == null)
      //  throw new Exception($"Setting with key {key} not found");

      //if (predicat(setting.Value, out T result))
      //  return result;

      throw new Exception($"Setting with key {key} has wrong value. Value {setting.Value}");
    }
  }

  public delegate bool TryParseHandler<T>(string value, out T result);
}
}
