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