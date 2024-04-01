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

using Microsoft.EntityFrameworkCore;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.Scheduler;

namespace SECODashBackend.Database;

public static class Extensions
{
   /// <summary>
   /// This method checks if the Ecosystems database exists. If it does not exist, create it and fill it with initial values.
   /// </summary>
   public static void CreateDbIfNotExists(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         ecosystemsContext.Database.EnsureCreated();
         var ecosystemService = services.GetRequiredService<IEcosystemsService>();
         UserDbInitializer.Initialize(ecosystemsContext);
         DbInitializer.Initialize(ecosystemsContext, ecosystemService);
      }
   }
   /// <summary>
   /// Schedule jobs for the ecosystems that exist on startup.
   /// </summary>
   public static void ScheduleInitialJobs(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         var scheduler = services.GetRequiredService<IScheduler>();
         var ecosystems = ecosystemsContext.Ecosystems.Include(ecosystem => ecosystem.Taxonomy).ToList();
         //Divide jobs evenly over the week with a 2 day interval.
         var dayIndex = 0;
         
         foreach (var ecosystem in ecosystems)
         {
            DayOfWeek scheduleDay = (DayOfWeek)dayIndex;
            
            var miningTerms = new List<string>();
            miningTerms.AddRange(ecosystem.Taxonomy.Select(tax => tax.Term));
            
            //Ensure the ecosystem name is included in the mined topics.
            if (!miningTerms.Contains(ecosystem.Name))
            {
               miningTerms.Add(ecosystem.Name);
            }
            scheduler.AddRecurringTaxonomyMiningJob(ecosystem.Name, miningTerms, 1000, 1000, scheduleDay);
            dayIndex = (dayIndex + 2) % 7;
         }
      }
   }
}