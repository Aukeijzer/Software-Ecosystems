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
    [DataMember(Name = "allTopics")] public required int AllTopics { get; init; }
    [DataMember(Name = "allProjects")] public required long AllProjects { get; init; }
    [DataMember(Name = "allContributors")] public required int AllContributors { get; init; }
    [DataMember(Name = "allContributions")] public required int AllContributions { get; init; }
}
   