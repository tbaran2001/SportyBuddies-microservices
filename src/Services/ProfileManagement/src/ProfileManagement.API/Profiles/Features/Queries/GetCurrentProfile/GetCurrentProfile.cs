using System.Security.Claims;
using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Queries.GetCurrentProfile;

public record GetCurrentProfileQuery : IQuery<GetCurrentProfileResult>;

public record GetCurrentProfileResult(ProfileDto Profile);

public record GetCurrentProfileResponseDto(ProfileDto Profile);

public class GetCurrentProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("test", (IHttpContextAccessor context) =>
        {
            var id = context?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Results.Content(id);
        });

        app.MapGet("profiles/me", async (ISender sender) =>
            {
                var query = new GetCurrentProfileQuery();

                var result = await sender.Send(query);

                var response = result.Adapt<GetCurrentProfileResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName(nameof(GetCurrentProfile))
            .Produces<GetCurrentProfileResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetCurrentProfile).Humanize())
            .WithDescription(nameof(GetCurrentProfile).Humanize());
    }
}

internal class GetCurrentProfileQueryHandler(
    ApplicationReadDbContext readDbContext,
    ICurrentUserProvider currentUserProvider) : IQueryHandler<GetCurrentProfileQuery, GetCurrentProfileResult>
{
    public async Task<GetCurrentProfileResult> Handle(GetCurrentProfileQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await readDbContext.Profiles
            .Find(p => p.ProfileId == currentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (profile is null)
            throw new ProfileNotFoundException(currentUserId);

        var profileDto = profile.Adapt<ProfileDto>();

        return new GetCurrentProfileResult(profileDto);
    }
}