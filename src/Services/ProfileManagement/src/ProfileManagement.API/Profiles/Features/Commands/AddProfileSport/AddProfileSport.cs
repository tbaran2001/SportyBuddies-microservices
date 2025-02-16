using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;

public record AddProfileSportCommand(Guid ProfileId, Guid SportId) : ICommand;

public record ProfileSportAddedDomainEvent(Guid ProfileId) : IDomainEvent;

public record AddProfileSportRequestDto(Guid SportId);

public class AddProfileSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("profiles/{profileId:guid}/sports",
                async (Guid profileId, AddProfileSportRequestDto request, ISender sender) =>
                {
                    var command = new AddProfileSportCommand(profileId, request.SportId);

                    await sender.Send(command);

                    return Results.NoContent();
                })
            .RequireAuthorization()
            .WithName(nameof(AddProfileSport))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(AddProfileSport).Humanize())
            .WithDescription(nameof(AddProfileSport).Humanize());
    }
}

public class AddProfileSportCommandValidator : AbstractValidator<AddProfileSportCommand>
{
    public AddProfileSportCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty().WithMessage("ProfileId is required.");
        RuleFor(x => x.SportId).NotEmpty().WithMessage("SportId is required.");
    }
}

internal class AddProfileSportCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProfileSportCommand>
{
    public async Task<Unit> Handle(AddProfileSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(command.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(command.ProfileId);

        profile.AddSport(command.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}