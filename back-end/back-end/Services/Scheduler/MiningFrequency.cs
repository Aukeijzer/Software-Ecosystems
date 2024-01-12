namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Enum that represents the frequency of mining projects.
/// </summary>
public enum MiningFrequency
{
    // TODO: remove Minutely in production
    Minutely,
    Hourly,
    Daily,
    Weekly,
}