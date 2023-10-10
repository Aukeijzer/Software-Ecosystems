using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Enums;

namespace SECODashBackend.Models;

[PrimaryKey(nameof(EcosystemId), nameof(Language))]
public class EcosystemProgrammingLanguage
{
    [DataMember(Name = "ecosystemId")]
    public required string EcosystemId { get; init; }
    
    [DataMember(Name = "language")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ProgrammingLanguage Language { get; init; }
    
    [DataMember(Name = "percentage")]
    public required float Percentage { get; init; }
}