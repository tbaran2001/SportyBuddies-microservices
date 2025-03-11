namespace ProfileManagement.API.Data.Repositories;

public interface IProfilesRepository
{
    Task<Profile> GetProfileByIdAsync(Guid profileId);
    Task<IEnumerable<Profile>> GetProfilesAsync();
    Task AddProfileAsync(Profile profile);
    void RemoveProfile(Profile profile);
    Task<IEnumerable<ProfileId>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> profileSports);
}