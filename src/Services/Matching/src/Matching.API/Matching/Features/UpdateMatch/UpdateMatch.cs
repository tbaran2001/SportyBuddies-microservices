﻿using Ardalis.GuardClauses;

namespace Matching.API.Matching.Features.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Swipe Swipe) : ICommand<UpdateMatchResult>;

public record UpdateMatchResult(Guid Id);

public record BothSwipedRightDomainEvent(Guid MatchId, Guid ProfileId, Guid MatchedProfileId) : IDomainEvent;

public record UpdateMatchRequestDto(Swipe Swipe);

public class UpdateMatchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/matches/{matchId}", async (Guid matchId, UpdateMatchRequestDto request, ISender sender) =>
            {
                var command = new UpdateMatchCommand(matchId, request.Swipe);

                await sender.Send(command);

                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithName(nameof(UpdateMatch))
            .Produces<UpdateMatchResult>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(UpdateMatch).Humanize())
            .WithDescription(nameof(UpdateMatch).Humanize());
    }
}

public class UpdateMatchCommandValidator : AbstractValidator<UpdateMatchCommand>
{
    public UpdateMatchCommandValidator()
    {
        RuleFor(x => x.MatchId).NotEmpty().WithMessage("MatchId is required.");
        RuleFor(x => x.Swipe).IsInEnum();
    }
}

internal class UpdateMatchCommandHandler(
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMatchCommand, UpdateMatchResult>
{
    public async Task<UpdateMatchResult> Handle(UpdateMatchCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var match = await matchesRepository.GetMatchByIdAsync(command.MatchId, cancellationToken);
        if (match is null)
            throw new MatchNotFoundException(command.MatchId);

        var oppositeMatch = await matchesRepository.GetMatchByIdAsync(match.OppositeMatchId, cancellationToken);
        if (oppositeMatch is null)
            throw new MatchNotFoundException(match.OppositeMatchId);

        match.SetSwipe(command.Swipe, oppositeMatch.Swipe);
        await unitOfWork.CommitChangesAsync();

        return new UpdateMatchResult(match.Id);
    }
}