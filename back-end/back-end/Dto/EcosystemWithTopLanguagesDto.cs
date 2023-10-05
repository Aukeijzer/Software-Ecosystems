using System.Runtime.Serialization;
using SECODashBackend.Enums;
using SECODashBackend.Models;

namespace SECODashBackend.Dto;

[DataContract]
public class EcosystemWithTopLanguagesDto
{
    [DataMember(Name = "ecosystem")]
    public Ecosystem Ecosystem { get; init; }
    
    [DataMember(Name = "toplanguages")]
    public List<ProjectProgrammingLanguage> TopLanguages { get; init; }
}