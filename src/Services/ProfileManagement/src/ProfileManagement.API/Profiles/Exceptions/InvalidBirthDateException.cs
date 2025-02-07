using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidBirthDateException(DateOnly birthDate)
    : BadRequestException($"Birth date: '{birthDate}' is invalid.");