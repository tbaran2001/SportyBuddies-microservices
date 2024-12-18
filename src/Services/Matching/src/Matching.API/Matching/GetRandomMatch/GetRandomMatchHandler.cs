namespace Matching.API.Matching.GetRandomMatch;

public record GetRandomMatchQuery(Guid ProfileId) : IQuery<GetRandomMatchResult>;

public record GetRandomMatchResult(Match Match);

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository)
    : IQueryHandler<GetRandomMatchQuery, GetRandomMatchResult>
{
    public async Task<GetRandomMatchResult> Handle(GetRandomMatchQuery request, CancellationToken cancellationToken)
    {
        var match = await matchesRepository.GetRandomMatchAsync(request.ProfileId, cancellationToken);

        return new GetRandomMatchResult(match);
    }
}