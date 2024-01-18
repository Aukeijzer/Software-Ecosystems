﻿using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Services.Analysis;

/// <summary>
/// Interface for services that analyse an ecosystem.
/// </summary>
public interface IAnalysisService
{
    /// <summary>
    /// Analyzes the ecosystem given by the topics.
    /// </summary>
    /// <param name="topics">The topics of the ecosystem.</param>
    /// <param name="technologies">The technologies of the</param>
    /// <param name="numberOfTopLanguages">The number of top languages to get.</param>
    /// <param name="numberOfTopSubEcosystems">The number of top sub ecosystems to get.</param>
    /// <param name="numberOfTopContributors">The number of top contributors to get.</param>
    /// <param name="numberOfTopProjects">The number of top projects to retrieve</param>
    /// <returns>The result of the analysis.</returns>
    public Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, List<string> technologies,
        int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors, int numberOfTopTechnologies, int numberOfTopProjects);
}