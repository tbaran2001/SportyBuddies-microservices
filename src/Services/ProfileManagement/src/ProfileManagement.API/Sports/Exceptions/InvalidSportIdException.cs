using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Sports.Exceptions;

public class InvalidSportIdException : BadRequestException
{
    public InvalidSportIdException(Guid sportId)
        : base($"Sport Id: '{sportId}' is invalid.")
    {
    }
}