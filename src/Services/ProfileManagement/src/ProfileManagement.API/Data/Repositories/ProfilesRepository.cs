namespace ProfileManagement.API.Data.Repositories;

public class ProfilesRepository(ApplicationDbContext dbContext) : IProfilesRepository
{
    public async Task<Profile> GetProfileByIdAsync(Guid profileId)
    {
        return await dbContext.Profiles
            .Include(u => u.ProfileSports)
            .FirstOrDefaultAsync(u => u.Id == profileId);
    }

    public async Task<IEnumerable<Profile>> GetProfilesAsync()
    {
        return await dbContext.Profiles.Include(p => p.ProfileSports).ToListAsync();
    }

    public async Task AddProfileAsync(Profile profile)
    {
        await dbContext.Profiles.AddAsync(profile);
    }

    public void RemoveProfile(Profile profile)
    {
        dbContext.Profiles.Remove(profile);
    }

    public async Task<IEnumerable<ProfileId>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> profileSports)
    {
        return await dbContext.Profiles
            .Where(u => u.Id != profileId)
            .Where(u => u.ProfileSports.Any(s => profileSports.Contains(s.SportId)))
            .Select(u => u.Id)
            .ToListAsync();
    }
}