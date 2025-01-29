using Ardalis.GuardClauses;
using Humanizer;
using Sport.API.Data.Repositories;
using Sport.API.Sports.Dtos;

namespace Sport.API.Sports.Features.GetSports;

public record GetSportsQuery : IQuery<GetSportsResult>;

public record GetSportsResult(IEnumerable<SportDto> Sports);

public record GetSportsResponseDto(IEnumerable<SportDto> Sports);

public class GetSportsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/sports", async (ISender sender) =>
            {
                var query = new GetSportsQuery();

                var result = await sender.Send(query);

                var response = result.Adapt<GetSportsResponseDto>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetSports))
            .Produces<GetSportsResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetSports).Humanize())
            .WithDescription(nameof(GetSports).Humanize());
    }
}

internal class GetSportsQueryHandler(ISportsRepository sportsRepository)
    : IQueryHandler<GetSportsQuery, GetSportsResult>
{
    public async Task<GetSportsResult> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var sports = await sportsRepository.GetSportsAsync(cancellationToken);

        var sportDtos = sports.Adapt<IEnumerable<SportDto>>();

        return new GetSportsResult(sportDtos);
    }
}