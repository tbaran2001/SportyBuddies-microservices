namespace Matching.API.Matching.GetRandomMatch;

public record GetRandomMatchResponse(Match Match);

public class GetRandomMatchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/matches/{profileId}/random", async (Guid profileId, ISender sender) =>
            {
                var query = new GetRandomMatchQuery(profileId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetRandomMatchResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRandomMatch")
            .Produces<GetRandomMatchResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get random match for a profile")
            .WithDescription("Get a random match for a profile by profile id");
    }
}