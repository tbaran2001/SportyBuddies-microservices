using Buddies.API.Buddies.ValueObjects;
using BuildingBlocks.Core.Model;

namespace Buddies.API.Buddies.Models;

public record Buddy : Aggregate<BuddyId>
{
    public Guid OppositeBuddyId { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid MatchedProfileId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static (Buddy, Buddy) CreatePair(Guid profileId, Guid matchedProfileId, DateTime createdOnUtc)
    {
        var buddy1 = new Buddy
        {
            Id = BuddyId.Of(Guid.NewGuid()),
            ProfileId = profileId,
            MatchedProfileId = matchedProfileId,
            CreatedOnUtc = createdOnUtc
        };
        var buddy2 = new Buddy
        {
            Id = BuddyId.Of(Guid.NewGuid()),
            ProfileId = matchedProfileId,
            MatchedProfileId = profileId,
            CreatedOnUtc = createdOnUtc
        };

        buddy1.OppositeBuddyId = buddy2.Id;
        buddy2.OppositeBuddyId = buddy1.Id;

        return (buddy1, buddy2);
    }

    private Buddy()
    {
    }
}