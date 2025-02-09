using ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;

namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileSportRemovedEventHandler(
    IProfilesRepository profilesRepository,
    ILogger<ProfileSportRemovedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<ProfileSportRemovedDomainEvent>
{
    public async Task Handle(ProfileSportRemovedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var potentialMatches =
            await profilesRepository.GetPotentialMatchesAsync(domainEvent.ProfileId, domainEvent.SportIds);

        var profileSportAddedIntegrationEvent = new ProfileSportRemovedIntegrationEvent()
        {
            ProfileId = domainEvent.ProfileId,
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}