namespace Matching.API.Matching.Exceptions;

public class InvalidMatchedAtException(DateTime matchedAt)
    : BadRequestException($"Matched At: '{matchedAt}' is invalid.");