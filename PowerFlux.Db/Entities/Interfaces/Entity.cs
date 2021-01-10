using System.ComponentModel.DataAnnotations;

namespace PowerFlux.Db.Entities.Interfaces
{
  public abstract class Entity
  {
    public int Id { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
