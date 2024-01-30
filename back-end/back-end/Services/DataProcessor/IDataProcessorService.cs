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

using SECODashBackend.Dtos.Topic;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// Interface for the service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public interface IDataProcessorService
{
    /// <summary>
    /// Sends the readme data to the data processor and returns the resulting topics.
    /// </summary>
    /// <param name="readmeDtos">The readme data to be sent to the data processor.</param>
    /// <returns>The resulting topics.</returns>
    public Task<List<ProjectDto>> GetTopics(List<ProjectDto> projectDtos);
}