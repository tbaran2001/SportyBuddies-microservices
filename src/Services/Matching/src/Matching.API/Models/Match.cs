namespace Matching.API.Models;

public class Match
{
    public Guid Id { get; private set; }
    public Guid OppositeMatchId { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid MatchedProfileId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    private Match(
        Guid id,
        Guid profileId,
        Guid matchedProfileId,
        DateTime matchDateTime
    )
    {
        Id = id;
        ProfileId = profileId;
        MatchedProfileId = matchedProfileId;
        MatchDateTime = matchDateTime;
    }

    public static (Match, Match) CreatePair(Guid profileId, Guid matchedProfileId, DateTime matchDateTime)
    {
        var match1 = new Match(Guid.NewGuid(), profileId, matchedProfileId, matchDateTime);
        var match2 = new Match(Guid.NewGuid(), matchedProfileId, profileId, matchDateTime);

        match1.OppositeMatchId = match2.Id;
        match2.OppositeMatchId = match1.Id;

        return (match1, match2);
    }

    public void SetSwipe(Swipe swipe)
    {
        Swipe = swipe;
        SwipeDateTime = DateTime.UtcNow;
    }

    public Match(
        Guid id,
        Guid oppositeMatchId,
        Guid profileId,
        Guid matchedProfileId,
        DateTime matchDateTime,
        Swipe? swipe,
        DateTime? swipeDateTime
    )
    {
        Id = id;
        OppositeMatchId = oppositeMatchId;
        ProfileId = profileId;
        MatchedProfileId = matchedProfileId;
        MatchDateTime = matchDateTime;
        Swipe = swipe;
        SwipeDateTime = swipeDateTime;
    }
}