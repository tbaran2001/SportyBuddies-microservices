using Microsoft.EntityFrameworkCore;

namespace Sport.API.Data;

public class SportDbContext(DbContextOptions<SportDbContext> options) : DbContext(options)
{
    public DbSet<Sports.Models.Sport> Sports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportDbContext).Assembly);
    }
}