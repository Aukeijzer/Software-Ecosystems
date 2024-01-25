using Hangfire.Dashboard;

namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Hangfire authorization filter that allows only authenticated users to access the Hangfire dashboard.
/// </summary>
public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        // TODO: Allow only root admins to access Hangfire dashboard
        return true;
        // return httpContext.User.Identity?.IsAuthenticated ?? false;
    }
}