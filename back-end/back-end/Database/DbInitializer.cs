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

using SECODashBackend.Services.Ecosystems;

namespace SECODashBackend.Database;

/// <summary>
/// Database initializer.
/// </summary>
public static class DbInitializer
{
   /// <summary>
   /// Initializes the Ecosystems database with the top-level ecosystems.
   /// </summary>
   /// <param name="context">The database context.</param>
   /// <param name="ecosystemsService"></param>
   public static void Initialize(EcosystemsContext context, IEcosystemsService ecosystemsService)
   {
      var ecosystem = context.Ecosystems.FirstOrDefault();
      if (ecosystem != null) return;
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.agriculture)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.agriculture)).Wait();
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.quantum)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.quantum)).Wait();
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.artificialintelligence)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.artificialintelligence)).Wait();
   }
}