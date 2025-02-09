namespace ProfileManagement.API.Profiles.Exceptions;

public class InvalidNameException(string name) : BadRequestException($"Name: '{name}' is invalid.");