namespace ProfileManagement.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfileCommand, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(command.Id);
        if (profile == null)
            throw new ProfileNotFoundException(command.Id);

        profile.Update(ProfileName.Create(command.Name), ProfileDescription.Create(command.Description),
            command.DateOfBirth, command.Gender);
        await unitOfWork.CommitChangesAsync();

        return profile.Adapt<ProfileResponse>();
    }
}