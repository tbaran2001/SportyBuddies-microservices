using Ardalis.GuardClauses;

namespace Sport.API.Sports.Features.CreateSport;

public record CreateSportCommand(string Name, string Description) : ICommand<CreateSportResult>;

public record CreateSportResult(Guid Id);

public record SportCreatedDomainEvent(Guid Id) : IDomainEvent;

public record CreateSportRequestDto(string Name, string Description);

public record CreateSportResponseDto(Guid Id);

public class CreateSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/sports", async (CreateSportRequestDto request, IMediator mediator) =>
            {
                var command = request.Adapt<CreateSportCommand>();

                var result = await mediator.Send(command);

                var response = result.Adapt<CreateSportResponseDto>();

                return Results.CreatedAtRoute("GetSportById", new { id = result.Id }, response);
            })
            .RequireAuthorization()
            .WithName(nameof(CreateSport))
            .Produces<CreateSportResponseDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(CreateSport).Humanize())
            .WithDescription(nameof(CreateSport).Humanize());
    }
}

public class CreateSportCommandValidator : AbstractValidator<CreateSportCommand>
{
    public CreateSportCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100)
            .WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500)
            .WithMessage("Description is required and must not exceed 500 characters.");
    }
}

internal class CreateSportCommandHandler(ISportsRepository sportsRepository)
    : ICommandHandler<CreateSportCommand, CreateSportResult>
{
    public async Task<CreateSportResult> Handle(CreateSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (await sportsRepository.SportExistsAsync(command.Name, cancellationToken))
            throw new SportAlreadyExistException(command.Name);

        var sportEntity = Models.Sport.Create(Name.Of(command.Name), Description.Of(command.Description));

        await sportsRepository.AddSportAsync(sportEntity, cancellationToken);

        return new CreateSportResult(sportEntity.Id);
    }
}