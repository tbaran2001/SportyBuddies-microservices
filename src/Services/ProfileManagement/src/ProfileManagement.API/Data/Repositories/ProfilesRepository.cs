using Microsoft.EntityFrameworkCore;
using ProfileManagement.API.Profiles.Models;

namespace ProfileManagement.API.Data.Repositories;

public class ProfilesRepository(ProfileDbContext dbContext) : IProfilesRepository
{
    public async Task<Profile?> GetProfileByIdAsync(Guid profileId)
    {
        return await dbContext.Profiles.FindAsync(profileId);
    }

    public async Task<Profile?> GetProfileByIdWithSportsAsync(Guid profileId)
    {
        return await dbContext.Profiles
            .Include(u => u.ProfileSports)
            .FirstOrDefaultAsync(u => u.Id == profileId);
    }

    public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
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

    public async Task<IEnumerable<Guid>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> profileSports)
    {
        return await dbContext.Profiles
            .Where(u => u.Id != profileId)
            .Where(u => u.ProfileSports.Any(s => profileSports.Contains(s.SportId)))
            .Select(u => u.Id)
            .ToListAsync();
    }
}