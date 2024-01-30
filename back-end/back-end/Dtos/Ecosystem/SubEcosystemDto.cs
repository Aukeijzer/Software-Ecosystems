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

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for a sub-ecosystem.
/// </summary>
public class SubEcosystemDto
{
    [DataMember(Name = "topic")] public required string Topic { get; init; }
    [DataMember(Name = "projectCount")] public required long ProjectCount { get; init; }

    public override bool Equals(object? obj)
    {
        return Topic == ((SubEcosystemDto) obj!).Topic && ProjectCount == ((SubEcosystemDto) obj).ProjectCount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Topic, ProjectCount);
    }
}