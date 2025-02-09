namespace Matching.API.Matching.Exceptions;

public class InvalidProfileIdException(Guid profileId) : BadRequestException($"Profile Id: '{profileId}' is invalid.");