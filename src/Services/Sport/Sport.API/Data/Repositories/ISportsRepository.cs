namespace Sport.API.Data.Repositories;

public interface ISportsRepository
{
    Task<Sports.Models.Sport?> GetSportByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Sports.Models.Sport>> GetSportsAsync(CancellationToken cancellationToken = default);
    Task AddSportAsync(Sports.Models.Sport sport, CancellationToken cancellationToken = default);
    Task<bool> SportExistsAsync(string name, CancellationToken cancellationToken = default);
}