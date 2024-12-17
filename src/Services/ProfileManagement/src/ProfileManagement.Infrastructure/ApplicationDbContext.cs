using Microsoft.EntityFrameworkCore;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Sport> Sports => Set<Sport>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<ProfileSport> ProfileSports => Set<ProfileSport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}