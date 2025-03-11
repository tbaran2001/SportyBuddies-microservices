namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileSportRemovedEventHandler(
    ILogger<ProfileSportRemovedEventHandler> logger,
    IPublishEndpoint publishEndpoint,
    ApplicationReadDbContext dbContext)
    : INotificationHandler<ProfileSportRemovedDomainEvent>
{
    public async Task Handle(ProfileSportRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var result = await dbContext.Profiles
            .FindOneAndUpdateAsync(
                Builders<ProfileReadModel>.Filter.Where(p => p.ProfileId == notification.ProfileId),
                Builders<ProfileReadModel>.Update.PullFilter(p => p.ProfileSports,
                    ps => ps.ProfileSportId == notification.Id),
                cancellationToken: cancellationToken);
        if (result is null)
            throw new ProfileNotFoundException(notification.ProfileId);

        var profileSportAddedIntegrationEvent = new ProfileSportRemovedIntegrationEvent()
        {
            ProfileId = notification.ProfileId,
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}