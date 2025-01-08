using Matching.API.Matching.Models;

namespace Matching.API.Data.Repositories;

public interface IMatchesRepository
{
    Task<Match> GetMatchByIdAsync(Guid matchId, CancellationToken cancellationToken = default);
    Task UpdateMatchAsync(Match match, CancellationToken cancellationToken = default);
    Task<IEnumerable<Match>> GetMatchesAsync(Guid profileId, CancellationToken cancellationToken = default);
    Task AddMatchesAsync(IEnumerable<Match> matches, CancellationToken cancellationToken = default);
    Task<Match> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default);
    Task<bool> CheckIfMatchExistsAsync(Guid profileId, Guid matchedProfileId, CancellationToken cancellationToken = default);
    Task RemoveMatchesAsync(Guid profileId, IEnumerable<Guid> potentialMatches);
}