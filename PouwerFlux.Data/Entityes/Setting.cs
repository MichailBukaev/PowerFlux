using PouwerFlux.Data.Entityes.Interfaces;

namespace PouwerFlux.Data.Entityes
{
  public class Setting : Entity
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public string DispleedName { get; set; }
  }
}
