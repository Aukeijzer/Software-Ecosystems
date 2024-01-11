namespace SECODashBackend.Dtos.users;
using System.Runtime.Serialization;

public class UserTypeDto
{
    [DataMember(Name = "userType")]
    public required string UserType { get; init; }
}