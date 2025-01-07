using Ardalis.GuardClauses;
using BuildingBlocks.Web;
using Matching.API.Matching.Dtos;

namespace Matching.API.Matching.Features.GetRandomMatch;

public record GetRandomMatchQuery : IQuery<GetRandomMatchResult>;

public record GetRandomMatchResult(MatchDto Match);

public record GetRandomMatchResponseDto(MatchDto Match);

public class GetRandomMatchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/matches/get-random-match", async (ISender sender) =>
            {
                var query = new GetRandomMatchQuery();

                var result = await sender.Send(query);

                var response = result.Adapt<GetRandomMatchResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("GetRandomMatch")
            .Produces<GetRandomMatchResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get random match for a current profile")
            .WithDescription("Get a random match for a profile by current profile id");
    }
}

internal class GetRandomMatchQueryHandler(
    IMatchesRepository matchesRepository,
    ICurrentUserProvider currentUserProvider)
    : IQueryHandler<GetRandomMatchQuery, GetRandomMatchResult>
{
    public async Task<GetRandomMatchResult> Handle(GetRandomMatchQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var match = await matchesRepository.GetRandomMatchAsync(currentUserId, cancellationToken);
        if (match is null)
            throw new RandomMatchNotFoundException();

        return new GetRandomMatchResult(match.Adapt<MatchDto>());
    }
}