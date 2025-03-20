using BuildingBlocks.EFCore;

namespace Sport.API.Data.Seed;

public class SportDataSeeder(ApplicationDbContext dbContext) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        await SeedSportAsync();
    }

    private async Task SeedSportAsync()
    {
        if (!await dbContext.Sports.AnyAsync())
        {
            await dbContext.Sports.AddRangeAsync(SportInitialData.GetInitialSports());
            await dbContext.SaveChangesAsync();
        }
    }
}