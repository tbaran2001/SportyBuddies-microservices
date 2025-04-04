﻿using Ardalis.GuardClauses;

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

internal class GetMatchesQueryHandler(
    IMatchesRepository matchesRepository)
    : IQueryHandler<GetMatchesQuery, GetMatchesResult>
{
    public async Task<GetMatchesResult> Handle(GetMatchesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        
        var matches = await matchesRepository.GetMatchesAsync(query.ProfileId, cancellationToken);

        var matchDtos = matches.Adapt<IEnumerable<MatchDto>>();

        return new GetMatchesResult(matchDtos);
    }
}