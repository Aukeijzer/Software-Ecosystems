using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Services.Analysis;

public interface IAnalysisService
{
    public Task<EcosystemDto> AnalyzeEcosystemAsync(
        List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems);
}