namespace ProfileManagement.API.Data.Repositories;

public interface ISportsRepository
{
    Task AddSportAsync(Sport sport);
    Task<bool> SportExistsAsync(Guid sportId);
}