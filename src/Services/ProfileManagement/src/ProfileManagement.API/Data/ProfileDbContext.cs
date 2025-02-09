namespace ProfileManagement.API.Data;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ProfileSport> ProfileSports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}