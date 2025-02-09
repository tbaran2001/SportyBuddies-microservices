namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidProfileSportIdException(Guid sportId) : BadRequestException($"Sport Id: '{sportId}' is invalid.");