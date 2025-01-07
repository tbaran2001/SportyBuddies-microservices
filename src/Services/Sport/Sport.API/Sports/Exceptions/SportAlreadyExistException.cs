using BuildingBlocks.Exceptions;

namespace Sport.API.Sports.Exceptions;

public class SportAlreadyExistException() : ConflictException("Sport with given name already exist!");