namespace Chat.API.Conversations.ValueObjects;

public record CreatedAt
{
    public DateTime Value { get; }

    [JsonConstructor]
    private CreatedAt(DateTime value) => Value = value;

    public static CreatedAt Of(DateTime value)
    {
        if (value == default)
            throw new InvalidCreatedAtException(value);

        return new CreatedAt(value);
    }

    public static implicit operator DateTime(CreatedAt createdAt) => createdAt.Value;
}