using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.FeatureManagement;

namespace ProfileManagement.Application.Profiles.EventHandlers;

public class ProfileSportAddedEventHandler(
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<ProfileSportAddedEventHandler> logger)
    : INotificationHandler<ProfileSportAddedEvent>
{
    public async Task Handle(ProfileSportAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (!await featureManager.IsEnabledAsync("Matching"))
        {
            var profileSportAddedIntegrationEvent = new ProfileSportAddedIntegrationEvent
            {
                ProfileId = domainEvent.ProfileId,
                SportId = domainEvent.SportId
            };
            await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
        }
    }
}