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
using SECODashBackend.Models;

namespace SECODashBackend.Database;
/// <summary>
/// This class is used to connect to the database, and create and update the all tables.
/// </summary>
public class EcosystemsContext(DbContextOptions<EcosystemsContext> options) : DbContext(options)
{
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.UseSerialColumns();
   }
   /// <summary>
   /// The Ecosystems table.
   /// </summary>
   public DbSet<Ecosystem> Ecosystems { get; set; }
   /// <summary>
   /// The Users table.
   /// </summary>
   public DbSet<User> Users { get; set; }
   /// <summary>
   /// The Taxonomy table.
   /// </summary>
   public DbSet<Taxonomy> Taxonomy { get; set; }
   /// <summary>
   /// The Technologies table.
   /// </summary>
   public DbSet<Technology> Technologies { get; set; }
   /// <summary>
   /// The BannedTopics table.
   /// </summary>
   public DbSet<BannedTopic> BannedTopics { get; set; }
}