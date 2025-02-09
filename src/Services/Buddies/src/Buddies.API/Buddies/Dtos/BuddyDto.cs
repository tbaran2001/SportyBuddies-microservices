namespace Buddies.API.Buddies.Dtos;

public record BuddyDto(Guid Id, Guid OppositeBuddyId, Guid ProfileId, Guid MatchedProfileId, DateTime CreatedAt);