using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PowerFlux
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(config =>
        {
          config.AddJsonFile("appsettings.json").Build();
        })
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));
  }
}