using BuildingBlocks.Core.Model;
using Microsoft.EntityFrameworkCore;
using ProfileManagement.API.Profiles.Models;

namespace ProfileManagement.API.Data;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ProfileSport> ProfileSports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}