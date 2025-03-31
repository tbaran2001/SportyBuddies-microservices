namespace ProfileManagement.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseDbContext(options)
{
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ProfileSport> ProfileSports { get; set; }
}