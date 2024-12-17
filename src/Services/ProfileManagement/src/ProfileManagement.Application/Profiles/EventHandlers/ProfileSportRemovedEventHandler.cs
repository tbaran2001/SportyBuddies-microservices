namespace ProfileManagement.Application.Profiles.EventHandlers;

public class ProfileSportRemovedEventHandler(ILogger<ProfileSportRemovedEventHandler> logger):INotificationHandler<ProfileSportRemovedEvent>
{
    public Task Handle(ProfileSportRemovedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
