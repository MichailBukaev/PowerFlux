using PouwerFlux.Data.Entityes.Interfaces;

namespace PouwerFlux.Context.Models
{
  public class Setting : Model
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public string DispleedName { get; set; }
  }
}
