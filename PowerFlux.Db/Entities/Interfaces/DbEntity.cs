using System.ComponentModel.DataAnnotations;

namespace PowerFlux.Db.ModelsDb.Interfaces
{
  public abstract class DbEntity
  {
    public int Id { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
