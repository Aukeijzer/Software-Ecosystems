using System.Runtime.Serialization;

namespace SECODashBackend.Dto;

[DataContract]
public class ProgrammingLanguageDto
{
    [DataMember(Name = "name")]
    public string Name { get; init; }
    
    [DataMember(Name = "percentage")]
    public float Percentage { get; init; }
}