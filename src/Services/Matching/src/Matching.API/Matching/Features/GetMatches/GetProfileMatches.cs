using Matching.API.Matching.Dtos;

namespace Matching.API.Matching.Features.GetMatches;

public record GetProfileMatchesQuery(Guid ProfileId) : IQuery<GetProfileMatchesResult>;

public record GetProfileMatchesResult(IEnumerable<MatchDto> MatchDtos);

public record GetProfileMatchesResponseDto(IEnumerable<MatchDto> MatchDtos);

public class GetProfileMatchesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/matches/{profileId}", async (Guid profileId, ISender sender) =>
            {
                var query = new GetProfileMatchesQuery(Guid.NewGuid());

                var result = await sender.Send(query);

                var response = result.Adapt<GetProfileMatchesResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("GetProfileMatches")
            .Produces<GetProfileMatchesResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get matches for a profile")
            .WithDescription("Get all matches for a profile by profile id");
    }
}

internal class GetProfileMatchesQueryHandler(
    IMatchesRepository matchesRepository)
    : IQueryHandler<GetProfileMatchesQuery, GetProfileMatchesResult>
{
    public async Task<GetProfileMatchesResult> Handle(GetProfileMatchesQuery query, CancellationToken cancellationToken)
    {
        var matches = await matchesRepository.GetMatchesAsync(query.ProfileId, cancellationToken);
        if (matches is null)
            throw new MatchesNotFoundException(query.ProfileId);

        var matchDtos = matches.Adapt<IEnumerable<MatchDto>>();

        return new GetProfileMatchesResult(matchDtos);
    }
}