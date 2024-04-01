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

namespace spider.Models.Graphql;

public class Repository
{
    public required string Name { get; init; }
    
    public required string Id { get; init; }
    
    public DateTime? PushedAt { get; init; }
    
    public required LatestRelease DefaultBranchRef { get; init; }
    
    public DateTime CreatedAt { get; init; }
    public required string Description { get; init; }
    
    public int StargazerCount { get; init; }
    public required Owner Owner { get; init; }
    public required TopicsWrapper RepositoryTopics { get; init; }
    public ReadMe? ReadmeCaps { get; init; }
    public ReadMe? ReadmeLower { get; init; }
    public ReadMe? ReadmeFstCaps { get; init; }
    public ReadMe? ReadmerstCaps { get; init; }
    public ReadMe? ReadmerstLower { get; init; }
    public ReadMe? ReadmerstFstCaps { get; init; }
    public required Languages Languages { get; init; }
}
public class RepositoryWrapper
{
    public Repository Repository { get; init; }
    
    public RateLimit RateLimit { get; init; }
}
