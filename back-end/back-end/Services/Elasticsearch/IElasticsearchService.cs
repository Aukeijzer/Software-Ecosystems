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

using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.ElasticSearch;

/// <summary>
/// Interface for the service that is responsible for handling all Elasticsearch related requests. 
/// </summary>
public interface IElasticsearchService
{
   /// <summary>
   /// Adds the given projects to the Elasticsearch index. 
   /// </summary>
   /// <param name="projectDtos">The projects to be added to the index.</param>
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   /// <summary>
   /// Queries the Elasticsearch index for projects that match the given search request. 
   /// </summary>
   /// <param name="searchRequest">The search request.</param>
   /// <returns>A SearchResponse for the projects that match the search request.</returns>
   public Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest);
   public Task<long> GetProjectCountByDate(DateTime startTime, List<string> topic);
}