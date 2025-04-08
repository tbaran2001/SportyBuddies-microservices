namespace Chat.API.Conversations.Exceptions;

public class InvalidCreatedAtException(DateTime createdAt) :
    BadRequestException($"Created at: '{createdAt}' is invalid.");