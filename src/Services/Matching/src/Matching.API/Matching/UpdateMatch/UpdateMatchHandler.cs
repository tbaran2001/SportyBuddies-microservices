using Buddies.Grpc;
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

public class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    BuddiesProtoService.BuddiesProtoServiceClient buddiesProto)
    : ICommandHandler<UpdateMatchCommand, UpdateMatchResult>
{
    public async Task<UpdateMatchResult> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        await buddiesProto.CreateBuddiesAsync(new CreateBuddiesRequest
        {
            ProfileId = Guid.NewGuid().ToString(),
            MatchedProfileId = Guid.NewGuid().ToString()
        }, cancellationToken: cancellationToken);

        var match = await matchesRepository.GetMatchByIdAsync(command.MatchId, cancellationToken);

        match.SetSwipe(command.Swipe);
        await matchesRepository.UpdateMatchAsync(match, cancellationToken);

        return new UpdateMatchResult(true);
    }
}