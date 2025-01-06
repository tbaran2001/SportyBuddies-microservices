using BuildingBlocks.Web;

namespace Matching.API.Matching.GetRandomMatch;

public record GetRandomMatchQuery() : IQuery<GetRandomMatchResult>;

public record GetRandomMatchResult(Match Match);

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, ICurrentUserProvider currentUserProvider)
    : IQueryHandler<GetRandomMatchQuery, GetRandomMatchResult>
{
    public async Task<GetRandomMatchResult> Handle(GetRandomMatchQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var match = await matchesRepository.GetRandomMatchAsync(currentUserId, cancellationToken);

        return new GetRandomMatchResult(match);
    }
}