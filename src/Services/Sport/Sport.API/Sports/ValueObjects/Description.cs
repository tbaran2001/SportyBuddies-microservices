using Sport.API.Sports.Exceptions;

namespace Sport.API.Sports.ValueObjects;

public record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDescriptionException(value);
        }

        return new Description(value);
    }

    public static implicit operator string(Description description)
    {
        return description.Value;
    }
}