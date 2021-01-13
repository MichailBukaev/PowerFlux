//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using PowerFlux.Db.Deploy;

//namespace ConsoleApp1
//{
//  class Program
//  {
//    static async Task Main(string[] args)
//    {
//      //using (var c = new UserContext())
//      //{
//      //  var user = new User
//      //  {
//      //    Name = "Jack",
//      //    Age = 20,
//      //  };
//      //  user.Cars = new List<Car>
//      //  {
//      //    new Car{Model = "Toyota", User = user},
//      //  };
//      //  c.Users.Add(user);
//      //  await c.SaveChangesAsync();
//      //}

//      using (var context = new UserContext())
//      {
//      //  //var users = await context.Users.ToListAsync();
//      //  //foreach (var user in users)
//      //  //{
//      //  //  Console.WriteLine($"User {user.Id} - {user.Name}");
//      //  //  foreach (var item in user.Cars)
//      //  //  {
//      //  //    Console.WriteLine($"  Car {item.Id} - {item.Model}");
//      //  //  }
//      //  //}
//      //  var q1 = new QQQ { Qqq = "qqq" };
//      //  var q2 = q1;
//      //  Console.WriteLine(ReferenceEquals(q1, q2));
//      //  var cars = await context.Cars.ToListAsync();
//      //  Console.WriteLine(ReferenceEquals(cars[0].User, cars[1].User));
//      //  foreach (var car in cars)
//      //  {
//      //    Console.WriteLine($"Car {car.Model}. User {car.User.Name}");
//      //  }
//      var cars = await context.Cars.Where(c => c.User.Name == "Jack").ToListAsync();
//      foreach (var car in cars)
//      {
//        Console.WriteLine($"{car.Model}");
//      }
//      }
//      UserService us = new UserService();
//      var users = await us.GetUsers(u => u.Name == "Tom");
//      foreach (var user in users)
//      {
//        Console.WriteLine($"{user.Id}{user.Name}");
//        user.Name = "Durak";
//        await us.Save();
//      }
//    }
//    public static async Task RefreshAll(UserContext ctx)
//    {
//      var col = ctx.ChangeTracker.Entries();
//      foreach (var entity in col)
//      {
//        await entity.ReloadAsync();
//      }
//    }
//  }
//  public class User
//  {
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public int Age { get; set; }
//    public virtual ICollection<Car> Cars { get; set; }
//    public User()
//    {
//      Cars = new List<Car>();
//    }
//  }
//  public class Car
//  {
//    public int Id { get; set; }
//    public string Model { get; set; }
//    public virtual User User { get; set; }
//  }

//  public class QQQ
//  {
//    public string Qqq { get; set; }
//  }
//  internal class UserContext : DbContext
//  {
//    internal UserContext()
//      : base("Server=.;Database=test_ssss;Trusted_Connection=True;")
//    {
//    }
//    public DbSet<User> Users { get; set; }
//    public DbSet<Car> Cars { get; set; }

//    internal virtual IQueryable<T> GetEntityes<T>() where T : class => Set<T>();
//  }

//  public class UserService
//  {
//    private readonly UserContext context;
//    public UserService()
//    {
//      context = new UserContext();
//    }
//    public async Task<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> wereExpression)
//    {
//      return await context.Users.Where(wereExpression).ToListAsync();
//    }

//    public Task Save() => context.SaveChangesAsync();
//  }

//  public class CarsService
//  {
//    public async Task<IEnumerable<Car>> GetCars()
//    {
//      using (var context = new UserContext())
//      {
//        UserService us= new UserService();
//        var users = await context.Cars.ToListAsync();
//        return users;
//      }
//    }
//  }
//}