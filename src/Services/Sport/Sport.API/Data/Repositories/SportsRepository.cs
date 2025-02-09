namespace Sport.API.Data.Repositories;

public class SportsRepository(ApplicationDbContext dbContext) : ISportsRepository
{
    public async Task<Sports.Models.Sport> GetSportByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sport = await dbContext.Sports.FindAsync(id, cancellationToken);

        return sport;
    }

    public async Task<IEnumerable<Sports.Models.Sport>> GetSportsAsync(CancellationToken cancellationToken = default)
    {
        var sports = await dbContext.Sports.ToListAsync(cancellationToken);

        return sports;
    }

    public async Task AddSportAsync(Sports.Models.Sport sport, CancellationToken cancellationToken = default)
    {
        await dbContext.Sports.AddAsync(sport, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SportExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Sports.AnyAsync(s => s.Name == name, cancellationToken);
    }
}