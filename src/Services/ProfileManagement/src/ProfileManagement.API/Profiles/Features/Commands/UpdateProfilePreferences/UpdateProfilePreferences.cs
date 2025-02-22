using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Commands.UpdateProfilePreferences;

public record UpdateProfilePreferencesCommand(
    Guid ProfileId,
    int MinAge,
    int MaxAge,
    int MaxDistance,
    Gender PreferredGender)
    : ICommand;

public record UpdateProfilePreferencesRequestDto(int MinAge, int MaxAge, int MaxDistance, Gender PreferredGender);

public class UpdateProfilePreferencesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("profiles/{profileId:guid}/preferences",
                async (Guid profileId, UpdateProfilePreferencesRequestDto request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProfilePreferencesCommand>() with { ProfileId = profileId };

                    await sender.Send(command);

                    return Results.NoContent();
                })
            .RequireAuthorization()
            .WithName(nameof(UpdateProfilePreferences))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(UpdateProfilePreferences).Humanize())
            .WithDescription(nameof(UpdateProfilePreferences).Humanize());
    }
}

public class UpdateProfilePreferencesCommandValidator : AbstractValidator<UpdateProfilePreferencesCommand>
{
    public UpdateProfilePreferencesCommandValidator()
    {
        RuleFor(x => x.ProfileId)
            .NotEmpty()
            .WithMessage("ProfileId is required.");
        RuleFor(x => x.MinAge)
            .InclusiveBetween(18, 100)
            .WithMessage("MinAge must be between 18 and 100.")
            .LessThanOrEqualTo(x => x.MaxAge)
            .WithMessage("MinAge cannot be greater than MaxAge.");
        RuleFor(x => x.MaxAge)
            .InclusiveBetween(18, 100)
            .WithMessage("MaxAge must be between 18 and 100.")
            .GreaterThanOrEqualTo(x => x.MinAge)
            .WithMessage("MaxAge cannot be smaller than MinAge.");
        RuleFor(x => x.MaxDistance)
            .InclusiveBetween(1, 100)
            .WithMessage("MaxDistance must be between 1 and 100.");
        RuleFor(x => x.PreferredGender)
            .IsInEnum();
    }
}

internal class UpdateProfilePreferencesCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProfilePreferencesCommand>
{
    public async Task<Unit> Handle(UpdateProfilePreferencesCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = await profilesRepository.GetProfileByIdAsync(command.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(command.ProfileId);

        profile.UpdatePreferences(Preferences.Of(command.MinAge, command.MaxAge, command.MaxDistance,
            command.PreferredGender));
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}