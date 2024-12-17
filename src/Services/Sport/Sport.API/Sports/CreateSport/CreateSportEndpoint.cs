namespace Sport.API.Sports.CreateSport;

public record CreateSportRequest(string Name, string Description);

public record CreateSportResponse(Guid Id);

public class CreateSportEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/sports", async (CreateSportRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateSportCommand>();

            var result = await sender.Send(command);

            var response=result.Adapt<CreateSportResponse>();

            return Results.Created($"/sports/{response.Id}", response);
        })
        .WithName("CreateSport")
        .Produces<CreateSportRequest>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Sport")
        .WithDescription("Create a new sport");
    }
}