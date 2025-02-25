using Ardalis.GuardClauses;

namespace ProfileManagement.API.Sports.Features.Commands;

public record CreateSportCommand(Guid SportId) : ICommand<CreateSportResult>;

public record CreateSportResult(SportDto Sport);

public class CreateSportCommandHandler(ISportsRepository sportsRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSportCommand, CreateSportResult>
{
    public async Task<CreateSportResult> Handle(CreateSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (await sportsRepository.SportExistsAsync(command.SportId))
            throw new SportAlreadyExistException(command.SportId);

        var sport = Sport.Create(SportId.Of(command.SportId));

        await sportsRepository.AddSportAsync(sport);
        await unitOfWork.CommitChangesAsync();

        return new CreateSportResult(sport.Adapt<SportDto>());
    }
}