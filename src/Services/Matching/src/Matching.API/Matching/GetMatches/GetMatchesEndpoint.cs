namespace Matching.API.Matching.GetMatches;

public record GetMatchesResponse(IEnumerable<Match> Matches);

public class GetMatchesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/matches/{profileId}", async (Guid profileId, ISender sender) =>
            {
                var query = new GetMatchesQuery(profileId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetMatchesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetMatches")
            .Produces<GetMatchesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get matches for a user")
            .WithDescription("Get all matches for a user by user id");
    }
}