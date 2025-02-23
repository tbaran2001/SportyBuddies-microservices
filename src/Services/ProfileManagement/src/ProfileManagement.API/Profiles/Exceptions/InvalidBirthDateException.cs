namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidBirthDateException(DateTime birthDate)
    : BadRequestException($"Birth date: '{birthDate}' is invalid.");