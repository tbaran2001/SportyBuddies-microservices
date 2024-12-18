namespace Matching.API.Matching.CreateMatches;

public record CreateMatchesCommand(Guid ProfileId) : ICommand;

public class CreateMatchesCommandHandler(IMatchesRepository matchesRepository) : ICommandHandler<CreateMatchesCommand>
{
    public async Task<Unit> Handle(CreateMatchesCommand command, CancellationToken cancellationToken)
    {
        var (match1, match2) = Match.CreatePair(command.ProfileId, Guid.NewGuid(), DateTime.Now);

        var matches = new List<Match> { match1, match2 };

        await matchesRepository.AddMatchesAsync(matches, cancellationToken);

        return Unit.Value;
    }
}

public class CreateMatchesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/matches", async (CreateMatchesCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.Created();
            })
            .WithName("CreateMatches")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create matches for a user")
            .WithDescription("Create matches for a user by user id");
    }
}