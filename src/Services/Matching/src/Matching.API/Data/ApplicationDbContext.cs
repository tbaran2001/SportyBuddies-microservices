namespace Matching.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseDbContext(options)
{
    public DbSet<Match> Matches { get; set; }

}