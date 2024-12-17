namespace Sport.API.Sports.GetSportById;

public record GetSportByIdResponse(Models.Sport Sport);

public class GetSportByIdEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/sports/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetSportByIdQuery(id));

            var response=result.Adapt<GetSportByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetSportById")
        .Produces<GetSportByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Sport by Id")
        .WithDescription("Get a sport by its id");
    }
}