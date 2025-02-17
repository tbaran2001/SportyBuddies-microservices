namespace ProfileManagement.API.Data.Repositories;

public class SportsRepository(ApplicationDbContext dbContext) : ISportsRepository
{
    public async Task<bool> SportExistsAsync(Guid sportId)
    {
        return await dbContext.Sports.AnyAsync(s => s.Id == sportId);
    }
}