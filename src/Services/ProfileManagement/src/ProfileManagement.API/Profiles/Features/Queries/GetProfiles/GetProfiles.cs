﻿using Ardalis.GuardClauses;
using ProfileManagement.API.Data.Repositories.Read;

namespace ProfileManagement.API.Profiles.Features.Queries.GetProfiles;

public record GetProfilesQuery : IQuery<GetProfilesResult>;

public record GetProfilesResult(IEnumerable<ProfileDto> Profiles);

public record GetProfilesResponseDto(IEnumerable<ProfileDto> Profiles);

public class GetProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("profiles", async (ISender sender) =>
            {
                var query = new GetProfilesQuery();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProfilesResponseDto>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetProfiles))
            .Produces<GetProfilesResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetProfiles).Humanize())
            .WithDescription(nameof(GetProfiles).Humanize());
    }
}

internal class GetProfilesQueryHandler(IProfilesReadRepository profilesReadRepository)
    : IQueryHandler<GetProfilesQuery, GetProfilesResult>
{
    public async Task<GetProfilesResult> Handle(GetProfilesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var profiles = await profilesReadRepository.GetProfilesAsync();

        var profileDtos = profiles.Adapt<IEnumerable<ProfileDto>>();

        return new GetProfilesResult(profileDtos);
    }
}