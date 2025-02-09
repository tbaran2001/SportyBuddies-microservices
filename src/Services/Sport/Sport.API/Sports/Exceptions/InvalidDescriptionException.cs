namespace Sport.API.Sports.Exceptions;

public class InvalidDescriptionException(string description)
    : BadRequestException($"Description: '{description}' is invalid.");