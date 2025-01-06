using BuildingBlocks.Web;

namespace ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;

public class RemoveSportFromProfileCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveSportFromProfileCommand>
{
    public async Task<Unit> Handle(RemoveSportFromProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.RemoveSport(request.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}