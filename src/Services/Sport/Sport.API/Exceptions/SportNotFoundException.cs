using BuildingBlocks.Exceptions;

namespace Sport.API.Exceptions;

public class SportNotFoundException(Guid id) : NotFoundException("Sport", id);