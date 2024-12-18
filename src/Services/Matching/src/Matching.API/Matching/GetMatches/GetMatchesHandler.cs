namespace Matching.API.Matching.GetMatches;

public record GetMatchesQuery(Guid UserId) : IQuery<GetMatchesResult>;

public record GetMatchesResult(IEnumerable<Match> Matches);

public class GetMatchesQueryHandler : IQueryHandler<GetMatchesQuery, GetMatchesResult>
{
    public Task<GetMatchesResult> Handle(GetMatchesQuery query, CancellationToken cancellationToken)
    {
        var (match1, match2) = Match.CreatePair(query.UserId, Guid.NewGuid(), DateTime.Now);
        var (match3, match4) = Match.CreatePair(query.UserId, Guid.NewGuid(), DateTime.Now);

        var matches = new List<Match> { match1, match2, match3, match4 };

        return Task.FromResult(new GetMatchesResult(matches));
    }
}