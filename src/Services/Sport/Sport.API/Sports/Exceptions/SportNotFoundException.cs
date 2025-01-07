using BuildingBlocks.Exceptions;

namespace Sport.API.Sports.Exceptions;

public class SportNotFoundException(Guid id) : NotFoundException("Sport", id);