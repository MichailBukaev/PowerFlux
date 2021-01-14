using System.ComponentModel.DataAnnotations;

namespace PouwerFlux.Data.Entityes.Interfaces
{
  public abstract class Model
  {
    public int Id { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
