namespace Matching.API.Matching.GetMatches;

public record GetMatchesQuery(Guid ProfileId) : IQuery<GetMatchesResult>;

public record GetMatchesResult(IEnumerable<Match> Matches);

public class GetMatchesQueryHandler(IMatchesRepository matchesRepository)
    : IQueryHandler<GetMatchesQuery, GetMatchesResult>
{
    public async Task<GetMatchesResult> Handle(GetMatchesQuery query, CancellationToken cancellationToken)
    {
        var matches = await matchesRepository.GetMatchesAsync(query.ProfileId, cancellationToken);

        return new GetMatchesResult(matches);
    }
}