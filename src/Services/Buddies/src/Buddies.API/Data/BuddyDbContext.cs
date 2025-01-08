using Buddies.API.Buddies.Models;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Data;

public class BuddyDbContext(DbContextOptions<BuddyDbContext> options) : DbContext(options)
{
    public DbSet<Buddy> Buddies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var profileId = Guid.NewGuid();
        var (buddy1, buddy2) = Buddy.CreatePair(profileId, Guid.NewGuid(), DateTime.UtcNow);
        var (buddy3, buddy4) = Buddy.CreatePair(profileId, Guid.NewGuid(), DateTime.UtcNow);

        modelBuilder.Entity<Buddy>().HasData(buddy1, buddy2, buddy3, buddy4);
    }
}