using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Enums;
using ProfileManagement.Domain.Events;
using ProfileManagement.Domain.Exceptions;
using ProfileManagement.Domain.ValueObjects;

namespace ProfileManagement.Domain.Models;

public class Profile : Entity
{
    public ProfileName Name { get; private set; } = default!;
    public ProfileDescription Description { get; private set; } = default!;
    public DateTime CreatedOnUtc { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public Gender Gender { get; private set; } = Gender.Unknown;
    public string? MainPhotoUrl { get; private set; }
    public Preferences Preferences { get; private set; } = default!;

    private readonly List<ProfileSport> _profileSports = new();
    public IReadOnlyList<ProfileSport> ProfileSports => _profileSports.AsReadOnly();

    public static Profile Create(
        Guid id,
        ProfileName name,
        ProfileDescription description,
        DateTime createdOnUtc,
        DateOnly dateOfBirth,
        Gender gender,
        Preferences preferences)
    {
        var profile = new Profile
        {
            Id = id,
            Name = name,
            Description = description,
            CreatedOnUtc = createdOnUtc,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            Preferences = preferences,
        };

        profile.AddDomainEvent(new ProfileCreatedEvent(profile.Id));

        return profile;
    }

    public static Profile CreateSimple(Guid id, ProfileName name, ProfileDescription description)
    {
        var profile = Create(
            id,
            name,
            description,
            DateTime.UtcNow,
            new DateOnly(1990, 1, 1),
            Gender.Unknown,
            Preferences.Default);

        return profile;
    }

    public void Update(
        ProfileName name,
        ProfileDescription description,
        DateOnly dateOfBirth,
        Gender gender)
    {
        Name = name;
        Description = description;
        DateOfBirth = dateOfBirth;
        Gender = gender;

        AddDomainEvent(new ProfileUpdatedEvent(Id));
    }

    public void AddSport(Guid sportId)
    {
        if (_profileSports.Any(s => s.SportId == sportId))
            throw new DomainException("Profile already has this sport.");

        _profileSports.Add(new ProfileSport(Id, sportId));
        AddDomainEvent(new ProfileSportAddedEvent(Id, _profileSports.Select(ps => ps.SportId).ToList()));
    }

    public void RemoveSport(Guid sportId)
    {
        var sport = _profileSports.FirstOrDefault(s => s.SportId == sportId);
        if (sport is null)
            throw new DomainException("Profile does not have this sport.");

        _profileSports.Remove(sport);
        AddDomainEvent(new ProfileSportRemovedEvent(Id, _profileSports.Select(ps => ps.SportId).ToList()));
    }

    public void UpdatePreferences(Preferences preferences)
    {
        Preferences = preferences;
    }
}