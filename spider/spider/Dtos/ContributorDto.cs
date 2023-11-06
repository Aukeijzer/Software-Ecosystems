﻿using System.Runtime.Serialization;

namespace spider.Dtos;

[DataContract]
public class ContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "id")]
    public required int Id { get; init; }
    [DataMember(Name = "NodeId")]
    public string? Node_id { get; init; }
    [DataMember(Name = "contributions")]
    public int? Contributions { get; init; }
    [DataMember(Name = "type")]
    public string? Type { get; init; }
}