using BuildingBlocks.Web;
using MassTransit;

namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public class AddSportToProfileCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddSportToProfileCommand>
{
    public async Task<Unit> Handle(AddSportToProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.AddSport(request.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}