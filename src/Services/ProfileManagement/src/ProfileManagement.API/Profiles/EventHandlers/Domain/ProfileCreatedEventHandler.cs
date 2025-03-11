namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileCreatedEventHandler(ILogger<ProfileCreatedEventHandler> logger, ApplicationReadDbContext dbContext)
    : INotificationHandler<ProfileCreatedDomainEvent>
{
    public async Task Handle(ProfileCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var profileReadModel = new ProfileReadModel
        {
            Id = Guid.NewGuid(),
            ProfileId = notification.Id,
            Name = notification.Name,
            Description = notification.Description,
            BirthDate = notification.BirthDate,
            Gender = notification.Gender,
            PreferencesMinAge = notification.Preferences.MinAge,
            PreferencesMaxAge = notification.Preferences.MaxAge,
            PreferencesMaxDistance = notification.Preferences.MaxDistance,
            PreferencesPreferredGender = notification.Preferences.PreferredGender,
            ProfileSports = new List<ProfileSportReadModel>()
        };

        var profile = await dbContext.Profiles
            .Find(p => p.ProfileId == notification.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (profile is not null)
            throw new ProfileAlreadyExistException();

        await dbContext.Profiles.InsertOneAsync(profileReadModel, cancellationToken: cancellationToken);

        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
    }
}