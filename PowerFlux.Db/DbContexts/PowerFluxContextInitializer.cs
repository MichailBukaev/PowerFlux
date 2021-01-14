using System.Collections.Generic;
using System.Data.Entity;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.DbContexts
{
  internal class PowerFluxContextInitializer : CreateDatabaseIfNotExists<PowerFluxContext>
  {
    protected override void Seed(PowerFluxContext db)
    {
      LoadSettings(db);
      LoadAlloyingElements(db);
    }

    private void LoadSettings(PowerFluxContext db)
    {
      var settings = new List<DbSetting>
      {
        new DbSetting {Key = "coatingMassCoefficient.max", Value = "0.45", DispleedName = "Max value of coating mass coefficient"},
        new DbSetting {Key = "coatingMassCoefficient.min", Value = "0.35", DispleedName = "Min value of coating mass coefficient"}
      };
      db.Set<DbSetting>().AddRange(settings);
      db.SaveChanges();
    }
    private void LoadAlloyingElements(PowerFluxContext db)
    {
      var elements = new List<DbAlloyingElement>
      {
        new DbAlloyingElement
        {
          Name = "Manganum",
          Symbol = "Mn",
          PartialTransformationToFerroalloyEquation = "1.30 - 2.15 * (P / S)",
          PartialTransformationToKernelEquation = "1.83 - 4.32 * (P / S)",
          PartialTransformationToGasEquation = "1.41 - 2.65 * (P / S)",
          PartialTransformationToSlagEquation = "0"
        },
        new DbAlloyingElement
        {
          Name = "Silicium",
          Symbol = "Si",
          PartialTransformationToFerroalloyEquation = "0.67 - 1.40 * (P / S)",
          PartialTransformationToKernelEquation = "0.85 - 2.0 * (P / S)",
          PartialTransformationToGasEquation = "0.85 - 2.0 * (P / S)",
          PartialTransformationToSlagEquation = "0"
        },
        new DbAlloyingElement
        {
          Name = "Carboneum",
          Symbol = "C",
          PartialTransformationToFerroalloyEquation = "2.81 - 6.86 * (P / S)",
          PartialTransformationToKernelEquation = "1.24 - 1.53 * (P / S)",
          PartialTransformationToGasEquation = "1.20 - 1.59 * (P / S)",
          PartialTransformationToSlagEquation = "0"
        },
        new DbAlloyingElement
        {
          Name = "Titanium",
          Symbol = "Ti",
          PartialTransformationToFerroalloyEquation = "0",
          PartialTransformationToKernelEquation = "0",
          PartialTransformationToGasEquation = "(25.9 * (P / S) - 2.74) * 0,0001",
          PartialTransformationToSlagEquation = "(25.9 * (P / S) - 2.74) * 0,0001"
        }
      };
      db.Set<DbAlloyingElement>().AddRange(elements);
      db.SaveChanges();
    }
  }
}
