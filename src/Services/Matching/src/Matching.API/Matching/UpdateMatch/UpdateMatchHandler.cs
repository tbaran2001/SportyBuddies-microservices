namespace Matching.API.Matching.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Swipe Swipe) : ICommand<UpdateMatchResult>;

public record UpdateMatchResult(bool IsSuccess);

public class UpdateMatchCommandHandler : ICommandHandler<UpdateMatchCommand, UpdateMatchResult>
{
    public Task<UpdateMatchResult> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new UpdateMatchResult(true));
    }
}