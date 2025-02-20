using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Matching.API.Data.Repositories;

public class CachedMatchesRepository(IMatchesRepository matchesRepository, IDistributedCache cache)
    : IMatchesRepository
{
    public async Task<Match> GetMatchByIdAsync(Guid matchId, CancellationToken cancellationToken = default)
    {
        return await matchesRepository.GetMatchByIdAsync(matchId, cancellationToken);
    }

    public async Task<IEnumerable<Match>> GetMatchesAsync(Guid? profileId,
        CancellationToken cancellationToken = default)
    {
        var cachedMatches = await cache.GetStringAsync(profileId.ToString(), cancellationToken);
        if (!string.IsNullOrEmpty(cachedMatches))
            return JsonSerializer.Deserialize<IEnumerable<Match>>(cachedMatches)!;

        var matches = await matchesRepository.GetMatchesAsync(profileId, cancellationToken);

        await cache.SetStringAsync(profileId.ToString(), JsonSerializer.Serialize(matches), cancellationToken);

        return matches;
    }

    public async Task<Match> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var randomMatchCacheKey = $"{profileId}_random";

        var cachedMatch = await cache.GetStringAsync(randomMatchCacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedMatch))
            return JsonSerializer.Deserialize<Match>(cachedMatch)!;

        var match = await matchesRepository.GetRandomMatchAsync(profileId, cancellationToken);

        await cache.SetStringAsync(randomMatchCacheKey, JsonSerializer.Serialize(match), cancellationToken);

        return match;
    }

    public async Task<bool> CheckIfMatchExistsAsync(Guid profileId, Guid matchedProfileId,
        CancellationToken cancellationToken = default)
    {
        return await matchesRepository.CheckIfMatchExistsAsync(profileId, matchedProfileId, cancellationToken);
    }

    public async Task RemoveMatchesAsync(Guid profileId, IEnumerable<Guid> potentialMatches)
    {
        await matchesRepository.RemoveMatchesAsync(profileId, potentialMatches);
    }


    public async Task AddMatchesAsync(IEnumerable<Match> matches, CancellationToken cancellationToken = default)
    {
        await matchesRepository.AddMatchesAsync(matches, cancellationToken);

        foreach (var match in matches)
        {
            var cachedMatches = await cache.GetStringAsync(match.ProfileId.ToString(), cancellationToken);
            if (string.IsNullOrEmpty(cachedMatches))
            {
                var matchAsList = new List<Match> { match };
                await cache.SetStringAsync(match.ProfileId.ToString(), JsonSerializer.Serialize(matchAsList),
                    cancellationToken);
                continue;
            }

            var cachedMatchesList = JsonSerializer.Deserialize<List<Match>>(cachedMatches)!;
            cachedMatchesList.Add(match);

            await cache.SetStringAsync(match.ProfileId.ToString(), JsonSerializer.Serialize(cachedMatchesList),
                cancellationToken);
        }
    }
}