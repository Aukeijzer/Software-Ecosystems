using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.User;

public class UpdatePermissionsDto
{
    [DataMember(Name = "userName")]
    public required string UserName { get; set; }
    
    [DataMember(Name = "userType")]
    public required Models.User.UserType UserType { get; init; }
}