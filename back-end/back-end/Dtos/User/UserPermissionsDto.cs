using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.User;

/// <summary>
/// This class represents the permissions a User has.
/// <see cref="UserType"/> is the permissions level.
/// <see cref="Ecosystems"/> is the list of ecosystem.
/// </summary>
public class UserPermissionsDto
{
    [DataMember(Name = "userType")]
    public required Models.User.UserType UserType { get; set; }

    [DataMember(Name = "ecosystems")] 
    public required List<string> Ecosystems { get; set; } = [];
}