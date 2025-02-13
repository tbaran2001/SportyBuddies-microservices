namespace Sport.API.Data.Repositories;

public class SportsRepository(ApplicationDbContext dbContext) : ISportsRepository
{
    public async Task<Sports.Models.Sport> GetSportByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Sports.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Sports.Models.Sport>> GetSportsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Sports.ToListAsync(cancellationToken);
    }

    public async Task AddSportAsync(Sports.Models.Sport sport, CancellationToken cancellationToken = default)
    {
        await dbContext.Sports.AddAsync(sport, cancellationToken);
    }

    public async Task<bool> SportExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Sports.AnyAsync(s => s.Name.Value == name, cancellationToken);
    }
}