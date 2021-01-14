using System.ComponentModel.DataAnnotations.Schema;
using PowerFlux.Db.ModelsDb.Interfaces;

namespace PowerFlux.Db.ModelsDb
{
  [Table("AlloyingElements")]
  public class DbAlloyingElement : DbEntity
  {
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string PartialTransformationToFerroalloyEquation { get; set; }
    public string PartialTransformationToKernelEquation { get; set; }
    public string PartialTransformationToGasEquation { get; set; }
    public string PartialTransformationToSlagEquation { get; set; }
  }
}
