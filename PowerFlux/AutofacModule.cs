using Autofac;
using PowerFlux.Db.DbContexts;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Db.Deploy;

namespace PowerFlux
{
  public class AutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DeployService>().As<IDeployService>().InstancePerLifetimeScope();

      builder.RegisterType<DbVersionContext>().As<IDbVersionContext>().InstancePerLifetimeScope();
      builder.RegisterType<AlloyingElementContext>().As<IAlloyingElementContext>();
      builder.RegisterType<AlloyingElementPartialTransformationEquationContext>().As<IAlloyingElementPartialTransformationEquationContext>();
      builder.RegisterType<SettingsContext>().As<ISettingsContext>();
    }
  }
}
