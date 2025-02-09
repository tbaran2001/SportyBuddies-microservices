namespace Matching.API.Matching.Models;

public record Match : Aggregate<MatchId>
{
    public MatchId OppositeMatchId { get; private set; }
    public ProfileId ProfileId { get; private set; }
    public ProfileId MatchedProfileId { get; private set; }
    public MatchedAt MatchedAt { get; private set; }
    public Swipe Swipe { get; private set; }
    public SwipedAt SwipedAt { get; private set; }

    public static (Match, Match) CreatePair(ProfileId profileId, ProfileId matchedProfileId, MatchedAt matchedAt)
    {
        var match1 = new Match
        {
            Id = MatchId.Of(Guid.NewGuid()),
            ProfileId = profileId,
            MatchedProfileId = matchedProfileId,
            MatchedAt = matchedAt
        };
        var match2 = new Match
        {
            Id = MatchId.Of(Guid.NewGuid()),
            ProfileId = matchedProfileId,
            MatchedProfileId = profileId,
            MatchedAt = matchedAt
        };

        match1.OppositeMatchId = match2.Id;
        match2.OppositeMatchId = match1.Id;

        return (match1, match2);
    }

    public void SetSwipe(Swipe swipe, Swipe? oppositeMatchSwipe)
    {
        Swipe = swipe;
        SwipedAt = SwipedAt.Of(DateTime.Now);

        if (oppositeMatchSwipe != Swipe.Right)
            return;

        var domainEvent = new BothSwipedRightDomainEvent(Id, ProfileId, MatchedProfileId);
        AddDomainEvent(domainEvent);
    }

    private Match()
    {
    }
}