namespace Chat.API.Conversations.ValueObjects;

public record ParticipantId
{
    public Guid Value { get; }

    [JsonConstructor]
    private ParticipantId(Guid value) => Value = value;

    public static ParticipantId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidParticipantIdException(value);

        return new ParticipantId(value);
    }

    public static implicit operator Guid(ParticipantId participantId) => participantId.Value;
}