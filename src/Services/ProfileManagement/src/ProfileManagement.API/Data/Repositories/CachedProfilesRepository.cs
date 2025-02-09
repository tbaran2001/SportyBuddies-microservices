using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ProfileManagement.API.Data.Repositories;

public class CachedProfilesRepository(IProfilesRepository profilesRepository, IDistributedCache cache)
    : IProfilesRepository
{
    public async Task<Profile> GetProfileByIdAsync(Guid profileId)
    {
        var cachedProfile = await cache.GetStringAsync(profileId.ToString());
        if (!string.IsNullOrEmpty(cachedProfile))
            return JsonSerializer.Deserialize<Profile>(cachedProfile)!;

        var profile = await profilesRepository.GetProfileByIdAsync(profileId);

        await cache.SetStringAsync(profileId.ToString(), JsonSerializer.Serialize(profile));

        return profile;
    }

    public async Task<Profile> GetProfileByIdWithSportsAsync(Guid profileId)
    {
        var cachedProfile = await cache.GetStringAsync(profileId.ToString());
        if (!string.IsNullOrEmpty(cachedProfile))
            return JsonSerializer.Deserialize<Profile>(cachedProfile)!;

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(profileId);

        await cache.SetStringAsync(profileId.ToString(), JsonSerializer.Serialize(profile));

        return profile;
    }

    public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
    {
        var cachedProfiles = await cache.GetStringAsync("all");
        if (!string.IsNullOrEmpty(cachedProfiles))
            return JsonSerializer.Deserialize<IEnumerable<Profile>>(cachedProfiles)!;

        var profiles = await profilesRepository.GetAllProfilesAsync();

        await cache.SetStringAsync("all", JsonSerializer.Serialize(profiles));

        return profiles;
    }

    public async Task AddProfileAsync(Profile profile)
    {
        await profilesRepository.AddProfileAsync(profile);

        await cache.SetStringAsync(profile.Id.ToString(), JsonSerializer.Serialize(profile));
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