namespace ProfileManagement.API.Profiles.ValueObjects;

public record ProfileSportId
{
    public Guid Value { get; }

    private ProfileSportId(Guid value) => Value = value;

    public static ProfileSportId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidProfileSportIdException(value);

        return new ProfileSportId(value);
    }

    public static implicit operator Guid(ProfileSportId profileSportId) => profileSportId.Value;
}