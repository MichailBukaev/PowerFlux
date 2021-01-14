using Microsoft.EntityFrameworkCore;
using PowerFlux.Db.ModelsDb;

namespace PowerFlux.Db.DbContexts
{
	internal static class MigrationExtension
	{
		internal static void Seed(this ModelBuilder modelBuilder)
		{
			LoadSettings(modelBuilder);
			LoadAlloyingElements(modelBuilder);
		}

		private static void LoadSettings(ModelBuilder modelBuilder)
		{
			var model = modelBuilder.Entity<DbSetting>();
			model.ToTable("Settings");
			model.HasAlternateKey(s => new { s.Id, s.Key });
			model.Property(s => s.Key).IsRequired(true);
			model.Property(s => s.Value).IsRequired(true);

			modelBuilder.Entity<DbSetting>().HasData(
				new DbSetting[]
			  {
					new DbSetting {Id = 1, Key = "coatingMassCoefficient.max", Value = "0.45", DispleedName = "Max value of coating mass coefficient"},
				  new DbSetting {Id = 2, Key = "coatingMassCoefficient.min", Value = "0.35", DispleedName = "Min value of coating mass coefficient"},
		      new DbSetting {Id = 3, Key = "min", Value = "1", DispleedName = "Min"}
			  });
		}

		private static void LoadAlloyingElements(ModelBuilder modelBuilder)
		{
			var model = modelBuilder.Entity<DbAlloyingElement>();
			model.ToTable("AlloyingElements");
		  model.Property(e => e.Name).IsRequired(true);
			model.Property(e => e.Symbol).IsRequired(true);

			modelBuilder.Entity<DbAlloyingElement>().HasData(
				new DbAlloyingElement[]
				{
					new DbAlloyingElement
					{
						Id = 1,
						Name = "Manganum",
						Symbol = "Mn",
						PartialTransformationToFerroalloyEquation = "1.30 - 2.15 * (P / S)",
					  PartialTransformationToKernelEquation = "1.83 - 4.32 * (P / S)",
					  PartialTransformationToGasEquation = "1.41 - 2.65 * (P / S)",
					  PartialTransformationToSlagEquation = "0"
					},
					new DbAlloyingElement
					{
						Id = 2,
						Name = "Silicium",
					  Symbol = "Si",
					  PartialTransformationToFerroalloyEquation = "0.67 - 1.40 * (P / S)",
					  PartialTransformationToKernelEquation = "0.85 - 2.0 * (P / S)",
					  PartialTransformationToGasEquation = "0.85 - 2.0 * (P / S)",
					  PartialTransformationToSlagEquation = "0"
				  },
				  new DbAlloyingElement
				  {
				    Id = 3,
						Name = "Carboneum",
					  Symbol = "C",
					  PartialTransformationToFerroalloyEquation = "2.81 - 6.86 * (P / S)",
					  PartialTransformationToKernelEquation = "1.24 - 1.53 * (P / S)",
					  PartialTransformationToGasEquation = "1.20 - 1.59 * (P / S)",
					  PartialTransformationToSlagEquation = "0"
				  },
				  new DbAlloyingElement
				  {
						Id = 4,
						Name = "Titanium",
					  Symbol = "Ti",
					  PartialTransformationToFerroalloyEquation = "0",
					  PartialTransformationToKernelEquation = "0",
					  PartialTransformationToGasEquation = "(25.9 * (P / S) - 2.74) * 0,0001",
					  PartialTransformationToSlagEquation = "(25.9 * (P / S) - 2.74) * 0,0001"
				  }
				});
		}
	}
}
