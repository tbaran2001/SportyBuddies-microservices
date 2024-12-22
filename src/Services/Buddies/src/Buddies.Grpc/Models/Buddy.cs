namespace Buddies.Grpc.Models;

public class Buddy
{
    public Guid Id { get; set; }
    public Guid OppositeBuddyId { get; set; }
    public Guid ProfileId { get; set; }
    public Guid MatchedProfileId { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    private Buddy(
        Guid id,
        Guid profileId,
        Guid matchedProfileId,
        DateTime createdOnUtc
    )
    {
        Id = id;
        ProfileId = profileId;
        MatchedProfileId = matchedProfileId;
        CreatedOnUtc = createdOnUtc;
    }

    public static (Buddy, Buddy) CreatePair(Guid profileId, Guid matchedProfileId, DateTime createdOnUtc)
    {
        var buddy1 = new Buddy(Guid.NewGuid(), profileId, matchedProfileId, createdOnUtc);
        var buddy2 = new Buddy(Guid.NewGuid(), matchedProfileId, profileId, createdOnUtc);

        buddy1.OppositeBuddyId = buddy2.Id;
        buddy2.OppositeBuddyId = buddy1.Id;

        return (buddy1, buddy2);
    }

    public Buddy()
    {

    }
}