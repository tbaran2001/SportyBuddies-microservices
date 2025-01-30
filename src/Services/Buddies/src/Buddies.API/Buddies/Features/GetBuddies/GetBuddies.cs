using Ardalis.GuardClauses;
using Buddies.API.Buddies.Dtos;
using Buddies.API.Data;
using BuildingBlocks.CQRS;
using Carter;
using FluentValidation;
using Humanizer;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Buddies.Features.GetBuddies;

public record GetBuddiesQuery(Guid? ProfileId) : IQuery<GetBuddiesResult>;

public record GetBuddiesResult(IEnumerable<BuddyDto> Buddies);

public record GetBuddiesResponseDto(IEnumerable<BuddyDto> Buddies);

public class GetBuddiesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("buddies", async (Guid? profileId, ISender sender) =>
            {
                var query = new GetBuddiesQuery(profileId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetBuddiesResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName(nameof(GetBuddies))
            .Produces<GetBuddiesResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetBuddies).Humanize())
            .WithDescription(nameof(GetBuddies).Humanize());
    }
}

public class GetBuddiesQueryValidator : AbstractValidator<GetBuddiesQuery>
{
    public GetBuddiesQueryValidator()
    {
        RuleFor(x => x.ProfileId)
            .NotEmpty();
    }
}

internal class GetBuddiesQueryHandler(BuddyDbContext dbContext)
    : IQueryHandler<GetBuddiesQuery, GetBuddiesResult>
{
    public async Task<GetBuddiesResult> Handle(GetBuddiesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var buddies = dbContext.Buddies.AsQueryable();

        if (query.ProfileId.HasValue)
            buddies = buddies.Where(b => b.ProfileId == query.ProfileId);

        var buddyDtos = await buddies
            .ProjectToType<BuddyDto>()
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetBuddiesResult(buddyDtos);
    }
}