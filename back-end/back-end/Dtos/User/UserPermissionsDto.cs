using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.User;

public class UserPermissionsDto
{
    [DataMember(Name = "userType")]
    public required Models.User.UserType UserType { get; set; }

    [DataMember(Name = "ecosystems")] 
    public required List<string> Ecosystems { get; set; } = [];
}