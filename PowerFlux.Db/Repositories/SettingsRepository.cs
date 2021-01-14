using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerFlux.Db.DbContexts;
using PowerFlux.Db.ModelsDb;
using PowerFlux.Db.Repositories.Interfaces;

namespace PowerFlux.Db.Repositories
{
  public class SettingsRepository : BaseRepository<DbSetting>, ISettingsRepository
  {
    public SettingsRepository(DbContextOptions<PowerFluxContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public Task<DbSetting> GetSettingAsync(string key)
      => GetDbEntityAsync(settings => settings.Where(s => s.Key.Equals(key)));

    public Task<DbSetting> UpdateSettingAsync(int id, string value)
    {
      return UpdateEntityAsync(async dbSet =>
      {
        var setting = await dbSet.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (setting == null)
          throw new Exception($"Setting wit id {id} not found");

        setting.Value = value;
        return setting;
      });
    }
  }
}
