using PowerFlux.Db.Entities.Interfaces;

namespace PowerFlux.Db.Entities
{
  public class Settings : Entity
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public string DispleedName { get; set; }
  }
}
