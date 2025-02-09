namespace Matching.API.Matching.ValueObjects;

public record MatchedAt
{
    public DateTime Value { get; }

    private MatchedAt(DateTime value) => Value = value;

    public static MatchedAt Of(DateTime value)
    {
        if (value == default)
            throw new InvalidMatchedAtException(value);

        return new MatchedAt(value);
    }

    public static implicit operator DateTime(MatchedAt matchedAt) => matchedAt.Value;
}