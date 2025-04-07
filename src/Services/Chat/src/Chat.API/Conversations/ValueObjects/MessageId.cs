namespace Chat.API.Conversations.ValueObjects;

public record MessageId
{
    public Guid Value { get; }

    [JsonConstructor]
    private MessageId(Guid value) => Value = value;

    public static MessageId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidMessageIdException(value);

        return new MessageId(value);
    }

    public static implicit operator Guid(MessageId messageId) => messageId.Value;
}