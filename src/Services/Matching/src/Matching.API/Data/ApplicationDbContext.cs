using Microsoft.EntityFrameworkCore;

namespace Matching.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Match> Matches { get; set; }
}