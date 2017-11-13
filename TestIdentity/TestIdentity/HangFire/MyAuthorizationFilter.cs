using System;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;

namespace TestIdentity.HangFire
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}