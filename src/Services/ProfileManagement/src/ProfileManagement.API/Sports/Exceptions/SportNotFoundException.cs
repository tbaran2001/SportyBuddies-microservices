namespace ProfileManagement.API.Sports.Exceptions;

public class SportNotFoundException(Guid id) : NotFoundException("Sport", id);