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

using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverters;

/// <summary>
/// Converts Ecosystems to and from data transfer objects.
/// </summary> 
public static class EcosystemConverter
{
    /// <summary>
    /// Converts an Ecosystem to a data transfer object.
    /// </summary>
    public static EcosystemOverviewDto ToDto(Ecosystem ecosystem)
    {
        return new EcosystemOverviewDto
        {
            Name = ecosystem.Name,
            Description = ecosystem.Description,
            DisplayName = ecosystem.DisplayName,
            NumberOfStars = ecosystem.NumberOfStars,
        };
    }
}