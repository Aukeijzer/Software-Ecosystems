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

namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Interface for a scheduler that is responsible for scheduling jobs.
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic to mine by</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringTopicMiningJob(string topic, string ecosystem, int amount, MiningFrequency miningFrequency);
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword to mine by. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringKeywordMiningJob(string keyword, string ecosystem, int amount, MiningFrequency miningFrequency);
    /// <summary>
    /// Removes a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic belonging to the job that is to removed. </param>
    public void RemoveRecurringTopicMiningJob(string topic);
    /// <summary>
    /// Removes a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword belonging to the job that is to removed. </param>
    public void RemoveRecurringKeywordMiningJob(string keyword);
    /// <summary>
    /// Add a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystemName"> The name of the ecosystem. </param>
    /// <param name="taxonomy"> The taxonomy to mine by. </param>
    /// <param name="keywordAmount"> The amount of projects to mine for each term using keyword search. </param>
    /// <param name="topicAmount"> The amount of projects to mine for each term using topic search. </param>
    public void AddRecurringTaxonomyMiningJob(string ecosystemName, List<string> taxonomy, int keywordAmount, int topicAmount);
    /// <summary>
    /// Removes a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystem"> The ecosystem belonging to the job that is to removed. </param>
    public void RemoveRecurringTaxonomyMiningJob(string ecosystem);
}