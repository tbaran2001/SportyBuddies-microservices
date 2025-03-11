namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileUpdatedEventHandler(
    ILogger<ProfileUpdatedEventHandler> logger,
    ApplicationReadDbContext readDbContext)
    : INotificationHandler<ProfileUpdatedDomainEvent>
{
    public async Task Handle(ProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var result = await readDbContext.Profiles.UpdateOneAsync(p => p.ProfileId == notification.Id,
            Builders<ProfileReadModel>.Update
                .Set(p => p.Name, notification.Name)
                .Set(p => p.Description, notification.Description)
                .Set(p => p.BirthDate, notification.BirthDate)
                .Set(p => p.Gender, notification.Gender), cancellationToken: cancellationToken);

        if (result.MatchedCount == 0)
            throw new ProfileNotFoundException(notification.Id);

        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
    }
}