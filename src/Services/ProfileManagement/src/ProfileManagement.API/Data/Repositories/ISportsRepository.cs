namespace ProfileManagement.API.Data.Repositories;

public interface ISportsRepository
{
    Task<bool> SportExistsAsync(Guid sportId);
}