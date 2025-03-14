namespace Sport.API.Sports.EventHandlers.Domain;

public class SportCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<SportCreatedEventHandler> logger,ApplicationDbContext dbContext)
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
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}