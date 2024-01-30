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

using spider.Dtos;
using spider.Models.Graphql;

namespace spider.Converters;

/// <summary>
/// IGraphqlDataConverter converts GraphQL data to a format that is easier to work with.
/// </summary>
public interface IGraphqlDataConverter
{
    /// <summary>
    /// SearchToProjects converts a SpiderData object to a list of ProjectDto objects.
    /// </summary>
    /// <param name="search">The SpiderData that needs to be converted</param>
    /// <returns>The repositories from data in the form of List&lt;ProjectDto&gt;</returns>
    public List<ProjectDto> SearchToProjects(SpiderData search);

    /// <summary>
    /// TopicSearchToProjects converts a TopicSearchData object to a list of ProjectDto objects.
    /// </summary>
    /// <param name="data">The TopicSearchData that needs to be converted</param>
    /// <returns>The repositories from data in the form of List&lt;ProjectDto&gt;</returns>
    public List<ProjectDto> TopicSearchToProjects(TopicSearchData data);

    /// <summary>
    /// RepositoryToProject converts a single Repository to a ProjectDto object.
    /// </summary>
    /// <param name="repository">The repository to convert</param>
    /// <returns>The repository in the form of ProjectDto</returns>
    public ProjectDto RepositoryToProject(Repository repository);

}