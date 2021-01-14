using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using PouwerFlux.Context.Models;

namespace PouwerFlux.Context.Context
{
  public class DatContext : DbContext
  {
    public DatContext(IConfiguration configuration)
    : base(configuration.GetConnectionString("Database"))
    {
    }
    private DbSet<Setting> Settings { get; set; }
    private DbSet<AlloyingElement> AlloyingElements {get; set;}
  }
}
