using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;

namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileSportRemovedEventHandler(
    IProfilesRepository profilesRepository,
    ILogger<ProfileSportRemovedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<ProfileSportRemovedEvent>
{
    public async Task Handle(ProfileSportRemovedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var potentialMatches =
            await profilesRepository.GetPotentialMatchesAsync(domainEvent.ProfileId, domainEvent.SportIds);

        var profileSportAddedIntegrationEvent = new ProfileSportRemovedIntegrationEvent()
        {
            ProfileId = domainEvent.ProfileId,
            PotentialMatches = potentialMatches
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}