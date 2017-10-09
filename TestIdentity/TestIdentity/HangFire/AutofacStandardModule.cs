using Autofac;
using TestIdentity.DAL;

namespace TestIdentity.HangFire
{
    public class AutofacStandardModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequrringService>().As<IRequrringService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(StudentsRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
