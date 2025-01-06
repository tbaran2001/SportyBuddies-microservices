using BuildingBlocks.Web;

namespace ProfileManagement.Application.Profiles.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfilePreferencesCommand>
{
    public async Task<Unit> Handle(UpdateProfilePreferencesCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        var preferences = Preferences.Create(request.MinAge, request.MaxAge, request.MaxDistance, request.PreferredGender);

        profile.UpdatePreferences(preferences);

        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}