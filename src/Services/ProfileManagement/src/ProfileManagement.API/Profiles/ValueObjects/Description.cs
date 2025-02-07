using ProfileManagement.API.Profiles.Exceptions;

namespace ProfileManagement.API.Profiles.ValueObjects;

public record Description
{
    public string Value { get; }

    private Description(string value) => Value = value;

    public static Description Of(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidDescriptionException(description);

        return new Description(description);
    }

    public static implicit operator string(Description description) => description.Value;
}