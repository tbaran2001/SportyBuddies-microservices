namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileCreatedEventHandler(ILogger<ProfileCreatedEventHandler> logger) : INotificationHandler<ProfileCreatedEvent>
{
    public Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}