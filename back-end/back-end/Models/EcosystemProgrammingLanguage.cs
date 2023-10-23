using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Enums;

namespace SECODashBackend.Models;

public class EcosystemProgrammingLanguage
{
    [DataMember(Name = "language")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ProgrammingLanguage Language { get; init; }
    
    [DataMember(Name = "percentage")]
    public required float Percentage { get; set; }
}