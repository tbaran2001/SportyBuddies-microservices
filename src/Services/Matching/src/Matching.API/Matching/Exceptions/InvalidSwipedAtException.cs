namespace Matching.API.Matching.Exceptions;

public class InvalidSwipedAtException(DateTime swipedAt) : BadRequestException($"Swiped At: '{swipedAt}' is invalid.");