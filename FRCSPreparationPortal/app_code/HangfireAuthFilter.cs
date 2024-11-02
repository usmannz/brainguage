using System;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace FRCSPreparationPortal.API
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // AppSettings.BuildMode == BuildMode.Dev
            return true;
            //var httpContext = context.GetHttpContext();
            //return httpContext.User.Identity.IsAuthenticated;
        }

        
    }
}
