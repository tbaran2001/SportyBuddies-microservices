using BuildingBlocks.Authentication;

namespace ProfileManagement.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfileCommand, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser= userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (profile == null)
            throw new ProfileNotFoundException(currentUser.Id);

        profile.Update(ProfileName.Create(command.Name), ProfileDescription.Create(command.Description),
            command.DateOfBirth, command.Gender);
        await unitOfWork.CommitChangesAsync();

        return profile.Adapt<ProfileResponse>();
    }
}