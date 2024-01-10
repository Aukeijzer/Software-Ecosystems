using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Services.Analysis;

/// <summary>
/// Interface for services that analyse an ecosystem.
/// </summary>
public interface IAnalysisService
{
    public Task<EcosystemDto> AnalyzeEcosystemAsync(
        List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors, int numberOfTopTechnologies);
}