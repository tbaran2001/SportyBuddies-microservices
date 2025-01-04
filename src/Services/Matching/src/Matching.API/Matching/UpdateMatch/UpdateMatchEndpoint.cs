namespace Matching.API.Matching.UpdateMatch;

public record UpdateMatchRequest(Swipe Swipe);

public record UpdateMatchResponse(bool IsSuccess);

public class UpdateMatchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/matches/{matchId}", async (Guid matchId, UpdateMatchRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateMatchCommand>() with { MatchId = matchId };

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateMatchResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("UpdateMatch")
            .Produces<UpdateMatchResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a match")
            .WithDescription("Update a match by match id");
    }
}