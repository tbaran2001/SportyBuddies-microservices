﻿using Ardalis.GuardClauses;
using BuildingBlocks.CQRS;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Dtos;
using ProfileManagement.API.Profiles.Exceptions;

namespace ProfileManagement.API.Profiles.Features.Queries.GetProfileById;

public record GetProfileByIdQuery(Guid Id) : IQuery<GetProfileByIdResult>;

public record GetProfileByIdResult(ProfileDto Profile);

public record GetProfileByIdResponseDto(ProfileDto Profile);

public class GetProfileByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("profiles/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetProfileByIdQuery(id);

                var result = await sender.Send(query);

                return Results.Ok(new GetProfileByIdResponseDto(result.Profile));
            })
            .RequireAuthorization()
            .WithName("GetProfileById")
            .Produces<GetProfileByIdResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get a profile by id")
            .WithDescription("Get a profile by id");
    }
}

public class GetProfileByIdQueryValidator : AbstractValidator<GetProfileByIdQuery>
{
    public GetProfileByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

internal class GetProfileByIdQueryHandler(IProfilesRepository profilesRepository)
    : IQueryHandler<GetProfileByIdQuery, GetProfileByIdResult>
{
    public async Task<GetProfileByIdResult> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(query.Id);
        if (profile == null)
            throw new ProfileNotFoundException(query.Id);

        var profileDto = profile.Adapt<ProfileDto>();

        return new GetProfileByIdResult(profileDto);
    }
}