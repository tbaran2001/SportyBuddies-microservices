using FluentValidation;

namespace Matching.API.Matching.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Swipe Swipe) : ICommand<UpdateMatchResult>;

public record UpdateMatchResult(bool IsSuccess);

public class UpdateMatchCommandValidator : AbstractValidator<UpdateMatchCommand>
{
    public UpdateMatchCommandValidator()
    {
        RuleFor(x => x.MatchId).NotEmpty();
        RuleFor(x => x.Swipe).IsInEnum();
    }
}

public class UpdateMatchCommandHandler(IMatchesRepository matchesRepository) : ICommandHandler<UpdateMatchCommand, UpdateMatchResult>
{
    public async Task<UpdateMatchResult> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        await matchesRepository.UpdateMatchAsync(command.MatchId, command.Swipe, cancellationToken);

        return new UpdateMatchResult(true);
    }
}