using ProfileManagement.API.Profiles.Features.Commands.UpdateProfile;

namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileUpdatedEventHandler(ILogger<ProfileUpdatedEventHandler> logger)
    : INotificationHandler<ProfileUpdatedDomainEvent>
{
    public Task Handle(ProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}