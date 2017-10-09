using Hangfire;
using Hangfire.SqlServer;

namespace TestIdentity.HangFire
{
    public class ReccuringJobInitializer
    {
        public ReccuringJobInitializer(SqlServerStorage storage)
        {
            JobStorage.Current = storage;
        }

        public  void InitializeJobs()
        {
            RecurringJob.AddOrUpdate<IRequrringService>(x => x.CheckEducationDate(), Cron.Daily);
        }
    }
}