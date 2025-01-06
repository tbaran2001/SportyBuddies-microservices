namespace Matching.API.Exceptions;

public class MatchNotFoundException(Guid matchId) : NotFoundException("Match", matchId);