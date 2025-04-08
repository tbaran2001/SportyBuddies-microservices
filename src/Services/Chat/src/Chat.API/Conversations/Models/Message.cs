namespace Chat.API.Conversations.Models;

public record Message : Entity<MessageId>
{
    public Content Content { get; init; }
    public DateTime CreatedAt { get; init; }

    public static Message Create(
        MessageId messageId,
        Content content,
        CreatedAt createdAt)
    {
        var message = new Message
        {
            Id = messageId,
            Content = content,
            CreatedAt = createdAt
        };

        return message;
    }
}