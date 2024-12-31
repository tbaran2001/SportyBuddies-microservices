using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;

namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileSportRemovedEventHandler(
    ILogger<ProfileSportRemovedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<ProfileSportRemovedEvent>
{
    public async Task Handle(ProfileSportRemovedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var profileSportRemovedIntegrationEvent = new ProfileSportRemovedIntegrationEvent
        {
            ProfileId = domainEvent.ProfileId,
            SportId = domainEvent.SportId
        };
        await publishEndpoint.Publish(profileSportRemovedIntegrationEvent, cancellationToken);
    }
}