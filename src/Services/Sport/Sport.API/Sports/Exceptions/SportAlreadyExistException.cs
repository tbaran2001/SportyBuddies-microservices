namespace Sport.API.Sports.Exceptions;

public class SportAlreadyExistException(string name) : ConflictException($"Sport with name \"{name}\" already exist!");