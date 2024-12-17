namespace Sport.API.Sports.GetSports;

public record GetSportsResponse(IEnumerable<Models.Sport> Sports);
public class GetSportsEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/sports", async (ISender sender) =>
        {
            var query = new GetSportsQuery();
            var result = await sender.Send(query);
            var response = result.Adapt<GetSportsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetSports")
        .Produces<GetSportsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Sports")
        .WithDescription("Get all sports");
    }
}