namespace Matching.API.Matching.Dtos;

public record MatchDto(
    Guid Id,
    Guid OppositeMatchId,
    Guid ProfileId,
    Guid MatchedProfileId,
    DateTime MatchedAt,
    Swipe? Swipe,
    DateTime? SwipedAt);