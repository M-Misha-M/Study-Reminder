using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestIdentity.Startup))]
namespace TestIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-TestIdentity-20170912045124.mdf;Initial Catalog=aspnet-TestIdentity-20170912045124;Integrated Security=True");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            ConfigureAuth(app);
        }
    }
}
