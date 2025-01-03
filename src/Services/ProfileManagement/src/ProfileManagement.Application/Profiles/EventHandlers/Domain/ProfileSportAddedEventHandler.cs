using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;

namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileSportAddedEventHandler(
    IProfilesRepository profilesRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<ProfileSportAddedEventHandler> logger)
    : INotificationHandler<ProfileSportAddedEvent>
{
    public async Task Handle(ProfileSportAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var potentialMatches =
            await profilesRepository.GetPotentialMatchesAsync(domainEvent.ProfileId, domainEvent.SportIds);

        var profileSportAddedIntegrationEvent = new ProfileSportAddedIntegrationEvent
        {
            ProfileId = domainEvent.ProfileId,
            PotentialMatches = potentialMatches
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}