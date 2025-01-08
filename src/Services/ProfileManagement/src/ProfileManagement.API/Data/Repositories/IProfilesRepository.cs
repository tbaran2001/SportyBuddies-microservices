using ProfileManagement.API.Profiles.Models;

namespace ProfileManagement.API.Data.Repositories;

public interface IProfilesRepository
{
    Task<Profile> GetProfileByIdAsync(Guid profileId);
    Task<Profile> GetProfileByIdWithSportsAsync(Guid profileId);
    Task<IEnumerable<Profile>> GetAllProfilesAsync();
    Task AddProfileAsync(Profile profile);
    void RemoveProfile(Profile profile);
    Task<IEnumerable<Guid>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> profileSports);
}