using ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;
using ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;
using ProfileManagement.API.Profiles.Features.Commands.UpdateProfile;

namespace ProfileManagement.API.Profiles.Models;

public record Profile : Aggregate<ProfileId>
{
    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public BirthDate BirthDate { get; private set; } = default!;
    public Gender Gender { get; private set; }
    public Preferences Preferences { get; private set; } = default!;

    private readonly List<ProfileSport> _profileSports = new();
    public IReadOnlyList<ProfileSport> ProfileSports => _profileSports.AsReadOnly();

    public static Profile Create(
        ProfileId id,
        Name name,
        Description description,
        BirthDate dateOfBirth,
        Gender gender,
        Preferences preferences)
    {
        var profile = new Profile
        {
            Id = id,
            Name = name,
            Description = description,
            BirthDate = dateOfBirth,
            Gender = gender,
            Preferences = preferences,
        };

        profile.AddDomainEvent(new ProfileCreatedDomainEvent(profile.Id));

        return profile;
    }

    public static Profile CreateSimple(ProfileId id, Name name, Description description)
    {
        var profile = Create(
            id,
            name,
            description,
            BirthDate.Of(new DateOnly(1990, 1, 1)),
            Gender.Unknown,
            Preferences.Default);

        return profile;
    }

    public void Update(
        Name name,
        Description description,
        BirthDate dateOfBirth,
        Gender gender)
    {
        Name = name;
        Description = description;
        BirthDate = dateOfBirth;
        Gender = gender;

        AddDomainEvent(new ProfileUpdatedDomainEvent(Id));
    }

    public void AddSport(Guid sportId)
    {
        if (_profileSports.Any(s => s.SportId == sportId))
            throw new DomainException("Profile already has this sport.");

        _profileSports.Add(new ProfileSport(Id, SportId.Of(sportId)));
        AddDomainEvent(new ProfileSportAddedDomainEvent(Id));
    }

    public void RemoveSport(Guid sportId)
    {
        var sport = _profileSports.FirstOrDefault(s => s.SportId == sportId);
        if (sport is null)
            throw new DomainException("Profile does not have this sport.");

        _profileSports.Remove(sport);
        AddDomainEvent(new ProfileSportRemovedDomainEvent(Id));
    }

    public void UpdatePreferences(Preferences preferences)
    {
        Preferences = preferences;
    }
}