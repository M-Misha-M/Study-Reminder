using Autofac;
using Hangfire;
using Hangfire.SqlServer;
using Owin;

namespace TestIdentity.HangFire
{
    public  static class HangfireDependency
    {
        public static void InitializeHangfireDependencies(IAppBuilder app)
        {           
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacStandardModule());
            var container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            JobActivator.Current = new AutofacJobActivator(container);
            var storage = new SqlServerStorage("DefaultConnection");
            var initializer = new ReccuringJobInitializer(storage);
            initializer.InitializeJobs();
            JobStorage.Current = storage;
            app.UseHangfireServer(new BackgroundJobServerOptions(), storage);
            app.UseHangfireDashboard("/Hangfire", new DashboardOptions(), storage);
        }
    }
}