using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database; 

public class EcosystemsContext(DbContextOptions<EcosystemsContext> options) : DbContext(options)
{
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.UseSerialColumns();
   }
   public DbSet<Ecosystem> Ecosystems { get; set; }
   
}