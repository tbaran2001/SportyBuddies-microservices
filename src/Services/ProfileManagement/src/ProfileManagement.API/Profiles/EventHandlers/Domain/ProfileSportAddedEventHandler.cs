namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileSportAddedEventHandler(
    IPublishEndpoint publishEndpoint,
    ILogger<ProfileSportAddedEventHandler> logger,
    ApplicationReadDbContext readDbContext,
    IUnitOfWork unitOfWork)
    : INotificationHandler<ProfileSportAddedDomainEvent>
{
    public async Task Handle(ProfileSportAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var profileSportReadModel = new ProfileSportReadModel
        {
            Id = Guid.NewGuid(),
            ProfileSportId = notification.Id,
            ProfileId = notification.ProfileId,
            SportId = notification.SportId
        };

        var result = await readDbContext.Profiles
            .FindOneAndUpdateAsync(
                Builders<ProfileReadModel>.Filter.Where(p => p.ProfileId == notification.ProfileId),
                Builders<ProfileReadModel>.Update.Push(p => p.ProfileSports, profileSportReadModel),
                cancellationToken: cancellationToken);
        if (result is null)
            throw new ProfileNotFoundException(notification.ProfileId);

        var profileSportAddedIntegrationEvent = new ProfileSportAddedIntegrationEvent
        {
            ProfileId = notification.ProfileId,
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
        await unitOfWork.CommitChangesAsync();
    }
}