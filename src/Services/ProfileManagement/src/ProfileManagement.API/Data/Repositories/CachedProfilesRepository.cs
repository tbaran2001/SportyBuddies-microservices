using Microsoft.Extensions.Caching.Distributed;

namespace ProfileManagement.API.Data.Repositories;

public class CachedProfilesRepository(IProfilesRepository profilesRepository, IDistributedCache cache)
    : IProfilesRepository
{
    public async Task<Profile> GetProfileByIdAsync(Guid profileId)
    {
        var cachedProfile = await cache.GetStringAsync(profileId.ToString());
        if (!string.IsNullOrEmpty(cachedProfile))
            return JsonConvert.DeserializeObject<Profile>(cachedProfile)!;

        var profile = await profilesRepository.GetProfileByIdAsync(profileId);

        await cache.SetStringAsync(profileId.ToString(), JsonConvert.SerializeObject(profile));

        return profile;
    }

    public async Task<IEnumerable<Profile>> GetProfilesAsync()
    {
        var cachedProfiles = await cache.GetStringAsync("all");
        if (!string.IsNullOrEmpty(cachedProfiles))
            return JsonConvert.DeserializeObject<IEnumerable<Profile>>(cachedProfiles)!;

        var profiles = await profilesRepository.GetProfilesAsync();

        await cache.SetStringAsync("all", JsonConvert.SerializeObject(profiles));

        return profiles;
    }

    public async Task AddProfileAsync(Profile profile)
    {
        await profilesRepository.AddProfileAsync(profile);

        await cache.SetStringAsync(profile.Id.ToString(), JsonConvert.SerializeObject(profile));
    }

    public void RemoveProfile(Profile profile)
    {
        profilesRepository.RemoveProfile(profile);

        cache.Remove(profile.Id.ToString());
    }

    public async Task<IEnumerable<ProfileId>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> profileSports)
    {
        return await profilesRepository.GetPotentialMatchesAsync(profileId, profileSports);
    }
}