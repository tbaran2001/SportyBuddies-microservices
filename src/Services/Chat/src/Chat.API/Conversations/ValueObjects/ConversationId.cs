namespace Chat.API.Conversations.ValueObjects;

public record ConversationId
{
    public Guid Value { get; }

    [JsonConstructor]
    private ConversationId(Guid value) => Value = value;

    public static ConversationId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidConversationIdException(value);

        return new ConversationId(value);
    }

    public static implicit operator Guid(ConversationId conversationId) => conversationId.Value;
}