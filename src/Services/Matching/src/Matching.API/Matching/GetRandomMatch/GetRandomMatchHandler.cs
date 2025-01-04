using BuildingBlocks.Authentication;

namespace Matching.API.Matching.GetRandomMatch;

public record GetRandomMatchQuery() : IQuery<GetRandomMatchResult>;

public record GetRandomMatchResult(Match Match);

public class GetRandomMatchQueryHandler(IMatchesRepository matchesRepository, IUserContext userContext)
    : IQueryHandler<GetRandomMatchQuery, GetRandomMatchResult>
{
    public async Task<GetRandomMatchResult> Handle(GetRandomMatchQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var match = await matchesRepository.GetRandomMatchAsync(currentUser.Id, cancellationToken);

        return new GetRandomMatchResult(match);
    }
}