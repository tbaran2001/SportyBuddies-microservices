using Matching.API.Matching.Exceptions;
using Matching.API.Matching.Models;
using Microsoft.EntityFrameworkCore;

namespace Matching.API.Data.Repositories;

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

    public async Task<Match?> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var randomMatch = await dbContext.Matches
            .Where(m => m.ProfileId == profileId && m.Swipe == null)
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefaultAsync(cancellationToken);

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