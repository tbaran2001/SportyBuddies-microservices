﻿namespace ProfileManagement.API.Profiles.ValueObjects;

public record ProfileId
{
    public Guid Value { get; }

    [JsonConstructor]
    private ProfileId(Guid value) => Value = value;

    public static ProfileId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidProfileIdException(value);

        return new ProfileId(value);
    }

    public static implicit operator Guid(ProfileId profileId) => profileId.Value;
}