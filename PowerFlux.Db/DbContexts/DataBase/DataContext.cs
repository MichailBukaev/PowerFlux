using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using PowerFlux.Db.Entities;

namespace PowerFlux.Db.DbContexts.DataBase
{
  public class DataContext : DbContext
  {
    private static DataContext _dataContext;
    private DataContext(IConfiguration configuration)
    : base(configuration.GetConnectionString("Database"))
    {
    }
    private DbSet<DbVersion> DbVersion { get; set; }
    private DbSet<Settings> Settings { get; set; }
    private DbSet<AlloyingElementPartialTransformationEquation> AlloyingElementPartialTransformationEquations { get; set; }
    private DbSet<AlloyingElement> AlloyingElements {get; set;}

    internal static DataContext GetContext(IConfiguration configuration)
    {
      return _dataContext ??= new DataContext(configuration);
    }
  }
}
