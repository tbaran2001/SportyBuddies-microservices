namespace Chat.API.Conversations.Exceptions;

public class InvalidConversationIdException(Guid conversationId) :
    Exception($"Conversation id {conversationId} is invalid.");