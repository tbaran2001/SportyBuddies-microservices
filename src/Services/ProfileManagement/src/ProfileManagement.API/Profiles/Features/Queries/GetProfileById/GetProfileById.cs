using Ardalis.GuardClauses;

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
            .WithName(nameof(GetProfileById))
            .Produces<GetProfileByIdResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary(nameof(GetProfileById).Humanize())
            .WithDescription(nameof(GetProfileById).Humanize());
    }
}

public class GetProfileByIdQueryValidator : AbstractValidator<GetProfileByIdQuery>
{
    public GetProfileByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
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