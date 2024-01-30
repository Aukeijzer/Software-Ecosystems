// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.Serialization;
using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Dtos.TimedData;

/// <summary>
/// Represents a data transfer object for a bucket that contains a list of topics with the number of active projects for a given time period.
/// </summary>
[DataContract]
public class TopicsBucketDto
{
    [DataMember(Name = "bucketDateLabel")] public string DateLabel { get; init; }
    [DataMember(Name = "topics")] public List<SubEcosystemDto> Topics { get; init; }
}