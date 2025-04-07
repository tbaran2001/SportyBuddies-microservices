namespace Chat.API.Conversations.Exceptions;

public class InvalidParticipantIdException(Guid participantId) :
    BadRequestException($"Participant id: '{participantId}' is invalid.");