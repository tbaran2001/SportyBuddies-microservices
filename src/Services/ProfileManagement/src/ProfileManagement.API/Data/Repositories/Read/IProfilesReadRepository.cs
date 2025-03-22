namespace ProfileManagement.API.Data.Repositories.Read;

public interface IProfilesReadRepository
{
    Task<ProfileReadModel> GetProfileByIdAsync(Guid id);
    Task<IEnumerable<ProfileReadModel>> GetProfilesAsync();
}