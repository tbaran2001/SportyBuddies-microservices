using BuildingBlocks.Exceptions;
using ProfileManagement.API.Profiles.Enums;

namespace ProfileManagement.API.Profiles.ValueObjects;

public record Preferences
{
    public static Preferences Default => new(18, 45, 50, 0);
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxDistance { get; }
    public Gender PreferredGender { get; }

    private Preferences(int minAge, int maxAge, int maxDistance, Gender preferredGender)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxDistance = maxDistance;
        PreferredGender = preferredGender;
    }

    public static Preferences Of(int minAge, int maxAge, int maxDistance, Gender preferredGender)
    {
        if (minAge < 0 || maxAge < 0)
            throw new DomainException("Age cannot be negative");
        if (minAge > maxAge)
            throw new DomainException("Min age cannot be greater than max age");
        if (maxDistance is < 1 or > 100)
            throw new DomainException("Max distance must be in range from 1 to 100");

        return new Preferences(minAge, maxAge, maxDistance, preferredGender);
    }
}