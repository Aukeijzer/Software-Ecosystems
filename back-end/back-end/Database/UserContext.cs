using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<User> Users { get; set; }
    
}