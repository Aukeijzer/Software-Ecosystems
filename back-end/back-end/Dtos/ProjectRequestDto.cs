﻿using System.Runtime.Serialization;

namespace SECODashBackend.Dtos;

[DataContract]
public class ProjectRequestDto
{
    [DataMember(Name = "ownerName")] 
    public required string OwnerName { get; init; }
    
    [DataMember(Name = "repoName")] 
    public required string RepoName { get; init; }
}