using BuildingBlocks.Exceptions;

namespace Buddies.API.Buddies.Exceptions;

public class InvalidBuddyIdException(Guid buddyId) : BadRequestException($"Buddy Id: '{buddyId}' is invalid.");