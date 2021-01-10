using PowerFlux.Db.DbContexts.Interfaces;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.Settings
{

  public class SettingsService : ISettingsService
  {
    private readonly ISettingsContext _settingsContext;
    public SettingsService(ISettingsContext settingsContext)
    {
      _settingsContext = settingsContext;
    }
    public async Task<T> GetValueAs<T>(string key, TryParseHandler<T> predicat) where T : struct
    {
      var setting = await _settingsContext.Entities.FirstOrDefaultAsync(s => s.Key.Equals(key));
      if (setting == null)
        throw new Exception($"Setting with key {key} not found");

      if (predicat(setting.Value, out T result))
        return result;

      throw new Exception($"Setting with key {key} has wrong value. Value {setting.Value}");
    }
  }

  public delegate bool TryParseHandler<T>(string value, out T result);
}
