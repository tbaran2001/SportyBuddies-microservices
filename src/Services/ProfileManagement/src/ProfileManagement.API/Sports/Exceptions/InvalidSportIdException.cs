namespace ProfileManagement.API.Sports.Exceptions;

public class InvalidSportIdException(Guid sportId) : BadRequestException($"Sport Id: '{sportId}' is invalid.");