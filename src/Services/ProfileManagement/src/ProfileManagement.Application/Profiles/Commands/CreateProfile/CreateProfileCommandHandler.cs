namespace ProfileManagement.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler(IProfilesRepository profilesRepository, IUnitOfWork unitOfWork)
     : ICommandHandler<CreateProfileCommand, ProfileResponse>
 {
     public async Task<ProfileResponse> Handle(CreateProfileCommand command,
         CancellationToken cancellationToken)
     {
         var profile = Profile.CreateSimple(ProfileName.Create(command.Name), ProfileDescription.Create(command.Description));

         await profilesRepository.AddProfileAsync(profile);
         await unitOfWork.CommitChangesAsync();

         return profile.Adapt<ProfileResponse>();
     }
 }