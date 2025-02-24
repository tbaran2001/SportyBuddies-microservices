namespace ProfileManagement.API.Sports.Exceptions;

public class SportAlreadyExistException(Guid sportId) : ConflictException($"Sport with id \"{sportId}\" already exist!");