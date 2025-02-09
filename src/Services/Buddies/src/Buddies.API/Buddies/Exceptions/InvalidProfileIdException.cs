namespace Buddies.API.Buddies.Exceptions;

public class InvalidProfileIdException(Guid profileId) : BadRequestException($"Invalid profile id: {profileId}");