namespace Matching.API.Exceptions;

public class MatchesNotFoundException(Guid profileId)
    : NotFoundException($"Matches not found for profile with ID: {profileId}");