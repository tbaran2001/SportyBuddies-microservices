namespace Matching.API.Matching.Exceptions;

public class MatchNotFoundException(Guid matchId) : NotFoundException("Match", matchId);