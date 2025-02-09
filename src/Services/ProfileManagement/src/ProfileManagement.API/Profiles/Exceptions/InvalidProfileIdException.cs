namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidProfileIdException(Guid profileId) : BadRequestException($"Profile Id: '{profileId}' is invalid.");