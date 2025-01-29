using Ardalis.GuardClauses;
using Humanizer;
using Sport.API.Data.Repositories;
using Sport.API.Sports.Dtos;
using Sport.API.Sports.Exceptions;

namespace Sport.API.Sports.Features.GetSportById;

public record GetSportByIdQuery(Guid Id) : IQuery<GetSportByIdResult>;

public record GetSportByIdResult(SportDto Sport);

public record GetSportByIdResponseDto(SportDto Sport);

public class GetSportById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/sports/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetSportByIdQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetSportByIdResponseDto>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName(nameof(GetSportById))
            .Produces<GetSportByIdResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(GetSportById).Humanize())
            .WithDescription(nameof(GetSportById).Humanize());
    }
}

internal class GetSportByIdQueryHandler(ISportsRepository sportsRepository)
    : IQueryHandler<GetSportByIdQuery, GetSportByIdResult>
{
    public async Task<GetSportByIdResult> Handle(GetSportByIdQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var sport = await sportsRepository.GetSportByIdAsync(query.Id, cancellationToken);
        if (sport is null)
            throw new SportNotFoundException(query.Id);

        var sportDto = sport.Adapt<SportDto>();

        return new GetSportByIdResult(sportDto);
    }
}