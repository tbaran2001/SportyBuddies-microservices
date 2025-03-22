namespace ProfileManagement.API.Data.Repositories.Read;

public class ProfilesReadRepository(ApplicationReadDbContext readDbContext) : IProfilesReadRepository
{
    public async Task<ProfileReadModel> GetProfileByIdAsync(Guid id)
    {
        return await readDbContext.Profiles
            .Find(p => p.ProfileId == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProfileReadModel>> GetProfilesAsync()
    {
        return await readDbContext.Profiles
            .Find(_ => true)
            .ToListAsync();
    }
}