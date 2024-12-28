using BuildingBlocks.Authentication;

namespace ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;

public class RemoveSportFromProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveSportFromProfileCommand>
{
    public async Task<Unit> Handle(RemoveSportFromProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (profile == null)
            throw new ProfileNotFoundException(currentUser.Id);

        profile.RemoveSport(request.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}