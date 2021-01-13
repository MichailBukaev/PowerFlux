using System;
using System.Collections.Generic;
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
      //using (var c = new UserContext())
      //{
      //  var user = new User
      //  {
      //    Name = "Tom",
      //    Age = 10,
      //  };
      //  user.Cars = new List<Car>
      //  {
      //    new Car{Model = "BMW", User = user}
      //  };
      //  c.Users.Add(user);
      //  await c.SaveChangesAsync();
      //}

      using (var context = new UserContext())
      {
        var users = await context.Users.ToListAsync();
        foreach (var user in users)
        {
          Console.WriteLine($"User {user.Id} - {user.Name}");
          foreach (var item in user.Cars)
          {
            Console.WriteLine($"  Car {item.Id} - {item.Model}");
          }
        }
        var car = await context.Cars.FirstOrDefaultAsync();
        Console.WriteLine($"Car user {car.User.Name}");
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
  }
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public virtual ICollection<Car> Cars { get; set; }
    public User()
    {
      Cars = new List<Car>();
    }
  }
  public class Car
  {
    public int Id { get; set; }
    public string Model { get; set; }
    public virtual User User { get; set; }
  }

  public class UserContext : DbContext
  {
    public UserContext()
      : base("Server=.;Database=test_ssss;Trusted_Connection=True;")
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }

    public IQueryable<T> GetEntityes<T>() where T : class => Set<T>();
  }
}