namespace ProfileManagement.API.Data.Repositories;

public interface IProfilesReadRepository
{
    Task<ProfileReadModel> GetProfileByIdAsync(Guid id);
    Task<IEnumerable<ProfileReadModel>> GetProfilesAsync();
}