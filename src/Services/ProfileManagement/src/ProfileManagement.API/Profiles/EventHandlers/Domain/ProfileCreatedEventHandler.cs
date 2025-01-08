using MediatR;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileCreatedEventHandler(ILogger<ProfileCreatedEventHandler> logger)
    : INotificationHandler<ProfileCreatedDomainEvent>
{
    public Task Handle(ProfileCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}