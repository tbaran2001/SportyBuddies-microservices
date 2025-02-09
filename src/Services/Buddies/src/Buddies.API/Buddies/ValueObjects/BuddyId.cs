using Buddies.API.Buddies.Exceptions;

namespace Buddies.API.Buddies.ValueObjects;

public record BuddyId
{
    public Guid Value { get; }

    private BuddyId(Guid value) => Value = value;

    public static BuddyId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidBuddyIdException(value);
        }

        return new BuddyId(value);
    }

    public static implicit operator Guid(BuddyId buddyId) => buddyId.Value;
}