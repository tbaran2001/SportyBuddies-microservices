using Ardalis.GuardClauses;

namespace ProfileManagement.API.Sports.Features.Commands;

public record CreateSportCommand(Guid SportId) : ICommand<CreateSportResult>;

public record CreateSportResult(SportDto Sport);

public class CreateSportCommandHandler(ApplicationDbContext dbContext)
    : ICommandHandler<CreateSportCommand, CreateSportResult>
{
    public async Task<CreateSportResult> Handle(CreateSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var sport = Sport.Create(SportId.Of(command.SportId));

        await dbContext.Sports.AddAsync(sport, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateSportResult(sport.Adapt<SportDto>());
    }
}