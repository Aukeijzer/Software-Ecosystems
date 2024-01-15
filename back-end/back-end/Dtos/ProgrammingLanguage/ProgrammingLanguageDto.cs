using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.ProgrammingLanguage;

/// <summary>
/// Represents a data transfer object for a programming language used in an ecosystem.
/// </summary>
public class ProgrammingLanguageDto
{
    [DataMember(Name = "language")]
    public required string Language { get; init; }
    
    [DataMember(Name = "percentage")]
    public float Percentage { get; set; }
}