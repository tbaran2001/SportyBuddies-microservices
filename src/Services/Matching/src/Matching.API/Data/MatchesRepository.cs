using Microsoft.EntityFrameworkCore;

namespace Matching.API.Data;

public class MatchesRepository(ApplicationDbContext dbContext) : IMatchesRepository
{
    public async Task<Match> GetMatchByIdAsync(Guid matchId, CancellationToken cancellationToken = default)
    {
        var match = await dbContext.Matches.FindAsync(matchId, cancellationToken);
        if (match == null)
            throw new MatchNotFoundException(matchId);

        return match;
    }

    public async Task UpdateMatchAsync(Match match, CancellationToken cancellationToken = default)
    {
        dbContext.Matches.Update(match);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Match>> GetMatchesAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Matches
            .Where(m => m.ProfileId == profileId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches, CancellationToken cancellationToken = default)
    {
        await dbContext.Matches.AddRangeAsync(matches, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Match> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var count = await dbContext.Matches.CountAsync(m => m.ProfileId == profileId && m.Swipe == null,
            cancellationToken);
        if (count == 0)
            return null;

        var randomIndex = new Random().Next(count);

        var randomMatch = await dbContext.Matches
            .Where(m => m.ProfileId == profileId && m.Swipe == null)
            .Skip(randomIndex)
            .Take(1)
            .FirstOrDefaultAsync(cancellationToken);
        if (randomMatch == null)
            throw new MatchNotFoundException(profileId);

        return randomMatch;
    }

    public async Task<bool> CheckIfMatchExistsAsync(Guid profileId, Guid matchedProfileId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Matches
            .AnyAsync(m => m.ProfileId == profileId && m.MatchedProfileId == matchedProfileId ||
                           m.ProfileId == matchedProfileId && m.MatchedProfileId == profileId, cancellationToken);
    }

    public Task RemoveMatchesAsync(Guid profileId, IEnumerable<Guid> potentialMatches)
    {
        var matchesToRemove = dbContext.Matches
            .Where(m => m.ProfileId == profileId && !potentialMatches.Contains(m.MatchedProfileId) ||
                        m.MatchedProfileId == profileId && !potentialMatches.Contains(m.ProfileId));

        dbContext.Matches.RemoveRange(matchesToRemove);

        return Task.CompletedTask;
    }
}