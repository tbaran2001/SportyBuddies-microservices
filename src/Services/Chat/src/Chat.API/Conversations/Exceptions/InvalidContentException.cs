namespace Chat.API.Conversations.Exceptions;

public class InvalidContentException(string content) :
    BadRequestException($"Content: '{content}' is invalid.");