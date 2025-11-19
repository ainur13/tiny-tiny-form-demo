using Microsoft.EntityFrameworkCore;
using TinyTinyForm.Domain;

namespace TinyTinyForm.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Order>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}