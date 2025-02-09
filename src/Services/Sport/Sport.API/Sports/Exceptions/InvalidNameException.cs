namespace Sport.API.Sports.Exceptions;

public class InvalidNameException(string name) : BadRequestException($"Name: '{name}' is invalid.");