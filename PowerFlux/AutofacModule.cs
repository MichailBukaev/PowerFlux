using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PowerFlux.Db.DbContexts;
using PowerFlux.Db.Repositories;
using PowerFlux.Db.Repositories.Interfaces;

namespace PowerFlux
{
  public class AutofacModule : Module
  {
    private readonly IConfiguration _configuration;
    public AutofacModule(IConfiguration configuration)
		{
      _configuration = configuration;
		}
    protected override void Load(ContainerBuilder builder)
    {
      //builder.RegisterType<DeployService>().As<IDeployService>().InstancePerLifetimeScope();

      //builder.RegisterType<DbVersionContext>().As<IDbVersionContext>().InstancePerLifetimeScope();
      //builder.RegisterType<AlloyingElementContext>().As<IAlloyingElementContext>();
      //builder.RegisterType<AlloyingElementPartialTransformationEquationContext>().As<IAlloyingElementPartialTransformationEquationContext>();
      //builder.RegisterType<SettingsContext>().As<ISettingsContext>();

      var optionsBuilder = new DbContextOptionsBuilder<PowerFluxContext>();
      var options = optionsBuilder
          .UseSqlServer(_configuration.GetConnectionString("Database"))
          .Options;
      builder.RegisterInstance(options).AsSelf();
      builder.RegisterType<SettingsRepository>().As<ISettingsRepository>();
    }
  }
}
