namespace ProfileManagement.API.Profiles.Models.ReadModels;

public class ProfileReadModel
{
    public required Guid Id { get; init; }
    public required Guid ProfileId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTime BirthDate { get; init; }
    public required Gender Gender { get; init; }

    public required int PreferencesMinAge { get; init; }
    public required int PreferencesMaxAge { get; init; }
    public required int PreferencesMaxDistance { get; init; }
    public required Gender PreferencesPreferredGender { get; init; }

    public required List<ProfileSportReadModel> ProfileSports { get; init; }
}