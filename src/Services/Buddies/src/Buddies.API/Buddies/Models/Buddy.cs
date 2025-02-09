using Buddies.API.Buddies.ValueObjects;
using BuildingBlocks.Core.Model;

namespace Buddies.API.Buddies.Models;

public record Buddy : Aggregate<BuddyId>
{
    public BuddyId OppositeBuddyId { get; private set; }
    public ProfileId ProfileId { get; private set; }
    public ProfileId MatchedProfileId { get; private set; }
    public CreatedAt CreatedAt { get; private set; }

    public static (Buddy, Buddy) CreatePair(ProfileId profileId, ProfileId matchedProfileId, CreatedAt createdAt)
    {
        var buddy1 = new Buddy
        {
            Id = BuddyId.Of(Guid.NewGuid()),
            ProfileId = profileId,
            MatchedProfileId = matchedProfileId,
            CreatedAt = createdAt
        };
        var buddy2 = new Buddy
        {
            Id = BuddyId.Of(Guid.NewGuid()),
            ProfileId = matchedProfileId,
            MatchedProfileId = profileId,
            CreatedAt = createdAt
        };

        buddy1.OppositeBuddyId = buddy2.Id;
        buddy2.OppositeBuddyId = buddy1.Id;

        return (buddy1, buddy2);
    }

    private Buddy()
    {
    }
}