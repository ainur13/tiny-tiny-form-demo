using Microsoft.EntityFrameworkCore;
using TinyTinyDynamicForm.Models;

namespace TinyTinyDynamicForm.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Submission> Submissions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Submission>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Submission>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}