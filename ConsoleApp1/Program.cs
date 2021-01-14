using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqToDB;

namespace ConsoleApp1
{
  class Program
  {
    static async Task  Main(string[] args)
    { 
      var repo = new repo();
      await using (var res = repo.GetContext(new UserContext()))
      {
        var user = await res.Set<User>().FirstOrDefaultAsync(u => u.Name == "Jack");
        if (user != null) user.Name = "Ivan";
      }
    }
  }
  
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
  }

  public class UserContext : DbContext
  {
    public UserContext()
      :base("Server=.;Database=test_ssss;Trusted_Connection=True;"){}

    public DbSet<User> Users { get; set; }
  }

  internal class repo
  {
    public Context GetContext(DbContext ctx)
    {
      return new Context(ctx);
    }
    internal class Context : UserContext, IAsyncDisposable
    {
      internal Context(DbContext dbContext)
      {
        
      }

      public async ValueTask DisposeAsync()
      {
        await SaveChangesAsync();
        Dispose();
      }
    }
  }


}