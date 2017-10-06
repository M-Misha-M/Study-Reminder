using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
