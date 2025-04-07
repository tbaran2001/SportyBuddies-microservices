namespace Chat.API.Conversations.Exceptions;

public class InvalidMessageIdException(Guid messageId) :
    BadRequestException($"Message id: '{messageId}' is invalid.");