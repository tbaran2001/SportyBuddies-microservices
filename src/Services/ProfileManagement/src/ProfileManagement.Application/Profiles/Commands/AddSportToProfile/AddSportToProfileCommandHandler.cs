namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public class AddSportToProfileCommandHandler(IProfilesRepository profilesRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<AddSportToProfileCommand>
{
    public async Task<Unit> Handle(AddSportToProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await profilesRepository.GetProfileByIdAsync(request.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(request.ProfileId);

        profile.AddSport(request.SportId);

        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}