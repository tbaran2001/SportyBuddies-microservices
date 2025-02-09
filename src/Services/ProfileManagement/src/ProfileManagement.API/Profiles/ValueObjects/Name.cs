namespace ProfileManagement.API.Profiles.ValueObjects;

public record Name
{
    public string Value { get; }

    private Name(string value) => Value = value;

    public static Name Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidNameException(value);

        return new Name(value);
    }

    public static implicit operator string(Name name) => name.Value;
}