using PowerFlux.Db.Entities.Interfaces;

namespace PowerFlux.Db.Entities
{
  public class DbVersion : Entity
  {
    public string Version { get; set; }
    public bool IsFilledDb { get; set; }
  }
}
