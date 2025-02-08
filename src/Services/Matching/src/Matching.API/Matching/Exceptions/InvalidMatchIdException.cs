namespace Matching.API.Matching.Exceptions;

public class InvalidMatchIdException(Guid matchId) : BadRequestException($"Match Id: '{matchId}' is invalid.");