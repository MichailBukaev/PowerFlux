using System.Threading.Tasks;
using PowerFlux.Db.Entities;

namespace PowerFlux.Db.Deploy
{
  public interface IDeployService
  {
    Task<DbVersion> Load();
  }
}