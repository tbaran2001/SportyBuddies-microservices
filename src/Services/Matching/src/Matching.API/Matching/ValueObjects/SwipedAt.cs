using Matching.API.Matching.Exceptions;

namespace Matching.API.Matching.ValueObjects;

public record SwipedAt
{
    public DateTime Value { get; }

    private SwipedAt(DateTime value)
    {
        Value = value;
    }

    public static SwipedAt Of(DateTime value)
    {
        if (value == default)
        {
            throw new InvalidSwipedAtException(value);
        }

        return new SwipedAt(value);
    }

    public static implicit operator DateTime(SwipedAt swipedAt)
    {
        return swipedAt.Value;
    }
}