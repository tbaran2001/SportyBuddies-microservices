﻿namespace Matching.API.Data.Repositories;

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

    public async Task<IEnumerable<Match>> GetMatchesAsync(Guid? profileId,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Matches.AsQueryable();

        if (profileId.HasValue)
            query = query.Where(m => m.ProfileId == profileId);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches, CancellationToken cancellationToken = default)
    {
        await dbContext.Matches.AddRangeAsync(matches, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Match> GetRandomMatchAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var randomMatch = await dbContext.Matches
            .Where(m => m.ProfileId == profileId && m.Swipe == Swipe.Unknown)
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

    public async Task RemoveMatchesAsync(Guid profileId, IEnumerable<Guid> potentialMatches)
    {
        var potentialMatchesList = potentialMatches.ToList();

        var matchesToRemove = await dbContext.Matches
            .Where(m =>
                (m.ProfileId == profileId && !potentialMatchesList.Contains(m.MatchedProfileId)) ||
                (m.MatchedProfileId == profileId && !potentialMatchesList.Contains(m.ProfileId))
            )
            .ToListAsync();

        if (matchesToRemove.Any())
        {
            dbContext.Matches.RemoveRange(matchesToRemove);
            await dbContext.SaveChangesAsync();
        }
    }
}