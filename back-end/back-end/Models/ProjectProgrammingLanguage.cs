using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SECODashBackend.Enums;

namespace SECODashBackend.Models;

[DataContract]
public class ProjectProgrammingLanguage
{
    [DataMember(Name = "id")]
    public required string Id { get; init; }
    
    [DataMember(Name = "language")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ProgrammingLanguage Language { get; init; }
    
    [DataMember(Name = "percentage")]
    public required float Percentage { get; init; }
}