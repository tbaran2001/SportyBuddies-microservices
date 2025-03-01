using Humanizer;
using Matching.API.Data.Repositories;

namespace Matching.API.Matching.Features.GetMatches;

public record GetMatchesQuery(Guid? ProfileId) : IQuery<GetMatchesResult>;

public record GetMatchesResult(IEnumerable<MatchDto> Matches);

public record GetMatchesResponseDto(IEnumerable<MatchDto> Matches);

public class GetMatchesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("matches", async (Guid? profileId, ISender sender) =>
            {
                var query = new GetMatchesQuery(profileId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetMatchesResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName(nameof(GetMatches))
            .Produces<GetMatchesResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetMatches).Humanize())
            .WithDescription(nameof(GetMatches).Humanize());
    }
}

public class GetMatchesQueryValidator : AbstractValidator<GetMatchesQuery>
{
    public GetMatchesQueryValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty().WithMessage("ProfileId is required.");
    }
}

internal class GetMatchesQueryHandler(
    IMatchesRepository matchesRepository)
    : IQueryHandler<GetMatchesQuery, GetMatchesResult>
{
    public async Task<GetMatchesResult> Handle(GetMatchesQuery query, CancellationToken cancellationToken)
    {
        var matches = await matchesRepository.GetMatchesAsync(query.ProfileId, cancellationToken);

        var matchDtos = matches.Adapt<IEnumerable<MatchDto>>();

        return new GetMatchesResult(matchDtos);
    }
}