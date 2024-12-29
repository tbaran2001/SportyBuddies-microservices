namespace ProfileManagement.Application.Profiles.EventHandlers.Domain;

public class ProfileUpdatedEventHandler(ILogger<ProfileUpdatedEventHandler> logger) : INotificationHandler<ProfileUpdatedEvent>
{
    public Task Handle(ProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}