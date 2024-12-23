using MassTransit;
using ProfileSportAddedEvent = BuildingBlocks.Messaging.Events.ProfileSportAddedEvent;

namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public class AddSportToProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint)
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