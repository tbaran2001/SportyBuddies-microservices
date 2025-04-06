namespace Chat.API.Conversations.Models;

public record Conversation : Aggregate<ConversationId>
{
    private readonly List<Participant> _participants = new();
    private readonly List<Message> _messages = new();
    public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public static Conversation CreateOneToOne(
        ConversationId id,
        Participant participant1,
        Participant participant2)
    {
        var conversation = new Conversation
        {
            Id = id,
            _participants = { participant1, participant2 }
        };

        // conversation.AddDomainEvent(new ConversationCreatedDomainEvent(conversation.Id, participant1, participant2));

        return conversation;
    }
}