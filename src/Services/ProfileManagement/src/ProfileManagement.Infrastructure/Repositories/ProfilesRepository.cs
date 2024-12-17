using Microsoft.EntityFrameworkCore;
using ProfileManagement.Domain.Interfaces;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Infrastructure.Repositories;

public class ProfilesRepository(ApplicationDbContext dbContext) : IProfilesRepository
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
}