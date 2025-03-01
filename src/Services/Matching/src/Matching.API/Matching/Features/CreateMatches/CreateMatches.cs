using Ardalis.GuardClauses;

namespace Matching.API.Matching.Features.CreateMatches;

public record CreateMatchesCommand(Guid ProfileId, Guid MatchedProfileId) : ICommand;

public class CreateMatchesCommandValidator : AbstractValidator<CreateMatchesCommand>
{
    public CreateMatchesCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty().WithMessage("ProfileId is required.");
        RuleFor(x => x.MatchedProfileId).NotEmpty().WithMessage("MatchedProfileId is required.");
    }
}

public class CreateMatchesCommandHandler(IMatchesRepository matchesRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMatchesCommand>
{
    public async Task<Unit> Handle(CreateMatchesCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (await matchesRepository.CheckIfMatchExistsAsync(command.ProfileId, command.MatchedProfileId,
                cancellationToken))
            return Unit.Value;

        var (match1, match2) = Match.CreatePair(
            ProfileId.Of(command.ProfileId),
            ProfileId.Of(command.MatchedProfileId),
            MatchedAt.Of(DateTime.Now));

        var matches = new List<Match> { match1, match2 };

        await matchesRepository.AddMatchesAsync(matches, cancellationToken);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}