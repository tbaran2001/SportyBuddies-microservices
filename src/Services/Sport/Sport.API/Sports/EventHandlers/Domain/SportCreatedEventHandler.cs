using BuildingBlocks.Events.Sports;
using MassTransit;
using Sport.API.Sports.Features.CreateSport;

namespace Sport.API.Sports.EventHandlers.Domain;

public class SportCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<SportCreatedEventHandler> logger)
    : INotificationHandler<SportCreatedDomainEvent>
{
    public async Task Handle(SportCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var sportCreatedIntegrationEvent = new SportCreatedIntegrationEvent
        {
            SportId = notification.Id
        };
        await publishEndpoint.Publish(sportCreatedIntegrationEvent, cancellationToken);
    }
}