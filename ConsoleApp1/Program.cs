using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PowerFlux.Db.Deploy;

namespace ConsoleApp1
{
  class Program
  {
    static async Task Main(string[] args)
    {
      using (var c = new UserContext())
      {
        var user = new User
        {
          Name = "Tom",
          Age = 10
        };
        c.Users.Add(user);
        await c.SaveChangesAsync();
        var tom = await c.GetEntityes<User>().FirstOrDefaultAsync(u => u.Name == "Tom");
        Console.WriteLine(c.Entry(tom).State);
        tom.Name = "Ivan";
        //c.Entry(tom).State = EntityState.Modified;
        Console.WriteLine(c.Entry(tom).State);
        await c.SaveChangesAsync();
        Console.WriteLine(c.Entry(tom).State);
        var usersAsync = await c.Users.ToListAsync();
        //Console.ReadLine();
        await RefreshAll(c);
        usersAsync = await c.Users.ToListAsync();
        await c.SaveChangesAsync();

      }


    }
    public static async Task RefreshAll(UserContext ctx)
    {
      var col = ctx.ChangeTracker.Entries();
      foreach (var entity in col)
      {
        await entity.ReloadAsync();
      }
    }
    public class  User
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
    }

    public class UserContext : DbContext
    {
      public UserContext()
        : base("Server=.;Database=test_ssss;Trusted_Connection=True;")
      {

      }
      public DbSet<User> Users { get; set; }

      public IQueryable<T> GetEntityes<T>() where T: class => Set<T>();
    }

  }
}