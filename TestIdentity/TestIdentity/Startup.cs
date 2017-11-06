using Hangfire;
using Autofac;
using Microsoft.Owin;
using Owin;
using TestIdentity.HangFire;

[assembly: OwinStartupAttribute(typeof(TestIdentity.Startup))]
namespace TestIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //GlobalConfiguration.Configuration
            //    .UseSqlServerStorage(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-TestIdentity-20170912045124.mdf;Initial Catalog=aspnet-TestIdentity-20170912045124;Integrated Security=True");
          
            //ReccuringJobInitializer.InitializeJobs();
            //app.UseHangfireServer();
            //app.UseHangfireDashboard();
            ////var builder = new ContainerBuilder();
            ////builder.RegisterType<RequrringService>().As<IRequrringService>();
            ////GlobalConfiguration.Configuration.UseAutofacActivator(builder.Build());



            ConfigureAuth(app);
            HangfireDependency.InitializeHangfireDependencies(app);
        }
    }
}
