using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.ProgrammingLanguage;

public class ProgrammingLanguageDto
{
    [DataMember(Name = "language")]
    public required string Language { get; init; }
    
    [DataMember(Name = "percentage")]
    public float Percentage { get; set; }
}