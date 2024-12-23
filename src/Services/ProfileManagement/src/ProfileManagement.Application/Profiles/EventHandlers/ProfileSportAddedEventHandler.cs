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

        if (!await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            var profileSportAddedIntegrationEvent = new
            {
                domainEvent.ProfileId,
                domainEvent.SportId
            };
            await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
        }
    }
}