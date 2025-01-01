using BuildingBlocks.Authentication;

namespace ProfileManagement.Application.Profiles.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesCommandHandler(
    IProfilesRepository profilesRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfilePreferencesCommand>
{
    public async Task<Unit> Handle(UpdateProfilePreferencesCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdAsync(currentUser.Id);
        if (profile == null)
            throw new ProfileNotFoundException(currentUser.Id);

        var preferences = Preferences.Create(request.MinAge, request.MaxAge, request.MaxDistance, request.PreferredGender);

        profile.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}