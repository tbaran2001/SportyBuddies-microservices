namespace Matching.API.Matching.CreateMatches;

public record CreateMatchesCommand(Guid ProfileId,Guid MatchedProfileId) : ICommand;

public class CreateMatchesCommandHandler(IMatchesRepository matchesRepository) : ICommandHandler<CreateMatchesCommand>
{
    public async Task<Unit> Handle(CreateMatchesCommand command, CancellationToken cancellationToken)
    {
        if(await matchesRepository.CheckIfMatchExistsAsync(command.ProfileId, command.MatchedProfileId, cancellationToken))
            return Unit.Value;

        var (match1, match2) = Match.CreatePair(command.ProfileId, command.MatchedProfileId, DateTime.Now);

        var matches = new List<Match> { match1, match2 };

        await matchesRepository.AddMatchesAsync(matches, cancellationToken);

        return Unit.Value;
    }
}