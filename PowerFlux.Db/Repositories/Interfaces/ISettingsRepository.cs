using System.Threading.Tasks;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.Repositories.Interfaces
{
  public interface ISettingsRepository
  {
    Task<DbSetting> GetSettingAsync(string key);
    Task<DbSetting> UpdateSettingAsync(int id, string value);
  }
}