using Ardalis.GuardClauses;
using Buddies.API.Buddies.Dtos;
using Buddies.API.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using Carter;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Buddies.Features.GetProfileBuddies;

public record GetProfileBuddiesQuery : IQuery<GetProfileBuddiesResult>;

public record GetProfileBuddiesResult(IEnumerable<BuddyDto> Buddies);

public record GetProfileBuddiesResponseDto(IEnumerable<BuddyDto> Buddies);

public class GetProfileBuddiesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("buddies", async (ISender sender) =>
            {
                var query = new GetProfileBuddiesQuery();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProfileBuddiesResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("GetProfileBuddies")
            .Produces<GetProfileBuddiesResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get profile buddies")
            .WithDescription("Get profile buddies of the current user");
    }
}

internal class GetProfileBuddiesQueryHandler(BuddyDbContext dbContext, ICurrentUserProvider currentUserProvider)
    : IQueryHandler<GetProfileBuddiesQuery, GetProfileBuddiesResult>
{
    public async Task<GetProfileBuddiesResult> Handle(GetProfileBuddiesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var buddies = await dbContext.Buddies
            .Where(b => b.ProfileId == currentUserId)
            .ProjectToType<BuddyDto>()
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetProfileBuddiesResult(buddies);
    }
}