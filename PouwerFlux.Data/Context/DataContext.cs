using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using PouwerFlux.Data.Entityes;

namespace PouwerFlux.Data.Context
{
  public class DataContext : DbContext
  {
    public DataContext(IConfiguration configuration)
    : base(configuration.GetConnectionString("Database"))
    {
    }
    private DbSet<Setting> Settings { get; set; }
    private DbSet<AlloyingElement> AlloyingElements {get; set;}
  }
}
