﻿using System.Runtime.Serialization;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.ProgrammingLanguage;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for an Ecosystem.
/// </summary>
public class EcosystemDto
{
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    [DataMember(Name = "description")] public string? Description { get; set; }
    [DataMember(Name = "numberOfStars")] public int? NumberOfStars { get; set; }
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "topLanguages")] public required List<ProgrammingLanguageDto> TopLanguages { get; init; }
    [DataMember(Name = "subEcosystems")] public required List<SubEcosystemDto> SubEcosystems { get; init; }
    [DataMember(Name = "topContributors")] public required List<TopContributorDto> TopContributors { get; init; }
    [DataMember(Name = "allTopics")] public int NumberOfTopics { get; init; }
    [DataMember(Name = "allProjects")] public long NumberOfProjects { get; init; }
    [DataMember(Name = "allContributors")] public int NumberOfContributors { get; init; }
    [DataMember(Name = "allContributions")] public int NumberOfContributions { get; init; }
    [DataMember(Name = "timedData")] public List<TimedDataDto>? TimedData { get; init; }
}
   