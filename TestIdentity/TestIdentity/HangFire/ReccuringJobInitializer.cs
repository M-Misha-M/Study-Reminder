using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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