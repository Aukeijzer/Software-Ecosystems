using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database;
/// <summary>
/// This class is used to connect to the database and create the Ecosystems table.
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
}