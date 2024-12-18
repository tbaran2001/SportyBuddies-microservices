namespace Matching.API.Exceptions;

public class MatchNotFoundException(Guid id) : NotFoundException("Match", id);