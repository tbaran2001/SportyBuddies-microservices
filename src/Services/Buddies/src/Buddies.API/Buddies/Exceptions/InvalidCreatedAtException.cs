namespace Buddies.API.Buddies.Exceptions;

public class InvalidCreatedAtException(DateTime createdAt) : BadRequestException($"Invalid created at: {createdAt}");