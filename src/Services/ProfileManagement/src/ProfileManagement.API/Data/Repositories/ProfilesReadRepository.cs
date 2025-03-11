namespace ProfileManagement.API.Data.Repositories;

public class ProfilesReadRepository(ApplicationReadDbContext readDbContext) : IProfilesReadRepository
{
    public async Task<ProfileReadModel> GetProfileByIdAsync(Guid id)
    {
        return await readDbContext.Profiles
            .Find(p => p.ProfileId == id)
            .FirstOrDefaultAsync();
    }
}