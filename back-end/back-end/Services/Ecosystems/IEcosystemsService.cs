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

namespace SECODashBackend.Services.Ecosystems;

/// <summary>
/// Interface for the service that is responsible for handling all ecosystem-related requests.
/// </summary>
public interface IEcosystemsService
{
   /// <summary>
   /// Returns all top-level ecosystems.
   /// </summary>
   /// <returns>All top-level ecosystems.</returns>
   public Task<List<EcosystemOverviewDto>> GetAllAsync();
   /// <summary>
   /// Returns the ecosystem that has the given topics.
   /// </summary>
   /// <param name="dto">The Dto that contains the request information of the ecosystem to get.</param>
   /// <returns>The ecosystem defined by the given topics.</returns>
   public Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto);
   public Task<string> UpdateDescription(DescriptionRequestDto dto);

   public Task<bool> CreateEcosystem(EcosystemCreationDto dto);

   /// <summary>
   /// This method returns the technology taxonomy of the given ecosystem.
   /// That is, the list of technologies that were used to create the ecosystem,
   /// and was saved in the postgres database when the ecosystem was created.
   /// </summary>
   /// <param name="ecosystemName">The name of the ecosystem we want to find technologies for.</param>
   /// <returns>A list of technologies for a given ecosystem.</returns>
   public Task<List<Technology>> GetTechnologyTaxonomy(string ecosystemName);

   public Task<string> UpdateTopics(EcosystemCreationDto dto);
   public Task<string> RemoveEcosystem(string ecosystem);
   public Task ScheduleEcosystem(string ecosystemName);
}