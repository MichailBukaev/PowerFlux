using System.ComponentModel.DataAnnotations.Schema;
using PowerFlux.Db.ModelsDb.Interfaces;

namespace PowerFlux.Db.ModelsDb
{
  [Table("Settings")]
  public class DbSetting : DbEntity
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public string DispleedName { get; set; }
  }
}
