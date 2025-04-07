namespace Chat.API.Conversations.Models;

public record Message : Entity<MessageId>
{
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }

    public static Message Create(
        MessageId messageId,
        string content,
        DateTime createdAt)
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