using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;

public record RemoveProfileSportCommand(Guid ProfileId, Guid SportId) : ICommand;

public record ProfileSportRemovedDomainEvent(Guid ProfileId) : IDomainEvent;

public class RemoveProfileSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("profiles/{profileId:guid}/sports/{sportId:guid}",
                async (Guid profileId, Guid sportId, ISender sender) =>
                {
                    var command = new RemoveProfileSportCommand(profileId, sportId);

                    await sender.Send(command);

                    return Results.NoContent();
                })
            .RequireAuthorization()
            .WithName(nameof(RemoveProfileSport))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(RemoveProfileSport).Humanize())
            .WithDescription(nameof(RemoveProfileSport).Humanize());
    }
}

public class RemoveProfileSportCommandValidator : AbstractValidator<RemoveProfileSportCommand>
{
    public RemoveProfileSportCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty().WithMessage("ProfileId is required.");
        RuleFor(x => x.SportId).NotEmpty().WithMessage("SportId is required.");
    }
}

internal class RemoveProfileSportCommandHandler(
    IProfilesRepository profilesRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProfileSportCommand>
{
    public async Task<Unit> Handle(RemoveProfileSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = await profilesRepository.GetProfileByIdAsync(command.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(command.ProfileId);

        if (!await sportsRepository.SportExistsAsync(command.SportId))
            throw new SportNotFoundException(command.SportId);

        profile.RemoveSport(SportId.Of(command.SportId));
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}