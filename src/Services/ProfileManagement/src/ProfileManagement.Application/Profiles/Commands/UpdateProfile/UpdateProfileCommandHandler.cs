using BuildingBlocks.Web;

namespace ProfileManagement.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfileCommand, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.Update(ProfileName.Create(command.Name), ProfileDescription.Create(command.Description),
            command.DateOfBirth, command.Gender);
        await unitOfWork.CommitChangesAsync();

        return profile.Adapt<ProfileResponse>();
    }
}