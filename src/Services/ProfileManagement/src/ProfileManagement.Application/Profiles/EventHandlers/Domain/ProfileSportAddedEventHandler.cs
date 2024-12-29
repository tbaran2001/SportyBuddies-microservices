using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileSportAddedEventHandler(
    IPublishEndpoint publishEndpoint,
    ILogger<ProfileSportAddedEventHandler> logger)
    : INotificationHandler<ProfileSportAddedEvent>
{
    public async Task Handle(ProfileSportAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var profileSportAddedIntegrationEvent = new ProfileSportAddedIntegrationEvent
        {
            ProfileId = domainEvent.ProfileId,
            SportId = domainEvent.SportId
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}