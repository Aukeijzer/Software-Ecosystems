using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

public class RemoveEcosystemDto
{
    [DataMember(Name = "ecosystem")] public string Ecosystem { get; set; }
}