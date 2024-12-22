namespace ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;

public class RemoveSportFromProfileCommandHandler(IProfilesRepository profilesRepository, IUnitOfWork unitOfWork): ICommandHandler<RemoveSportFromProfileCommand>
{
    public async Task<Unit> Handle(RemoveSportFromProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await profilesRepository.GetProfileByIdAsync(request.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(request.ProfileId);

        profile.RemoveSport(request.SportId);

        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}