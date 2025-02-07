using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidDescriptionException(string description)
    : BadRequestException($"Description: '{description}' is invalid.");