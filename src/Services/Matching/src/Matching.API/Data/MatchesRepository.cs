using Marten.Patching;

namespace Matching.API.Data;

public class MatchesRepository(IDocumentSession session) : IMatchesRepository
{
    public async Task<Match> GetMatchByIdAsync(Guid matchId, CancellationToken cancellationToken = default)
    {
        var match = await session.LoadAsync<Match>(matchId, cancellationToken);
        if (match == null)
            throw new MatchNotFoundException(matchId);

        return match;
    }

    public async Task UpdateMatchAsync(Guid matchId, Swipe swipe, CancellationToken cancellationToken = default)
    {
        session.Patch<Match>(matchId)
            .Set(m => m.Swipe, swipe)
            .Set(m => m.SwipeDateTime, DateTime.UtcNow);

        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Match>> GetMatchesAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var matches = await session.Query<Match>()
            .Where(m => m.ProfileId == profileId)
            .ToListAsync(cancellationToken);

        return matches;
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches, CancellationToken cancellationToken = default)
    {
        foreach (var match in matches)
        {
            session.Store(match);
        }

        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task<Match> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var count = await session.Query<Match>()
            .CountAsync(m => m.ProfileId == profileId && m.Swipe == null, cancellationToken);

        if (count == 0)
            throw new MatchesNotFoundException(profileId);

        var randomIndex = new Random().Next(count);

        var randomMatch = await session.Query<Match>()
            .Where(m => m.ProfileId == profileId && m.Swipe == null)
            .Skip(randomIndex)
            .Take(1)
            .FirstOrDefaultAsync(cancellationToken);
        if (randomMatch == null)
            throw new MatchesNotFoundException(profileId);

        return randomMatch;
    }
}