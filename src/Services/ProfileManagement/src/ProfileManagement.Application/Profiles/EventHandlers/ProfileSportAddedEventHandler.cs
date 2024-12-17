namespace ProfileManagement.Application.Profiles.EventHandlers;

public class ProfileSportAddedEventHandler(ILogger<ProfileSportAddedEventHandler> logger):INotificationHandler<ProfileSportAddedEvent>
{
    public Task Handle(ProfileSportAddedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}