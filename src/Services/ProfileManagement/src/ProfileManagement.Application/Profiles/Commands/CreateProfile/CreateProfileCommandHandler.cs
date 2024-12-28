using BuildingBlocks.Authentication;

namespace ProfileManagement.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProfileCommand, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(CreateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser().Id;

        var profile = Profile.CreateSimple(
            userId,
            ProfileName.Create(command.Name),
            ProfileDescription.Create(command.Description));

        await profilesRepository.AddProfileAsync(profile);
        await unitOfWork.CommitChangesAsync();

        return profile.Adapt<ProfileResponse>();
    }
}