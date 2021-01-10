using System.Threading.Tasks;

namespace PowerFlux.Services.Services.Settings
{
  public interface ISettingsService
  {
    Task<T> GetValueAs<T>(string key, TryParseHandler<T> predicat) where T : struct;
  }
}