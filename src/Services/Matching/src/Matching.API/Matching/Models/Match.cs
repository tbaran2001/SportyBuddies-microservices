﻿using BuildingBlocks.Core.Model;
using Matching.API.Matching.Features.UpdateMatch;

namespace Matching.API.Matching.Models;

public class Match : Entity
{
    public Guid OppositeMatchId { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid MatchedProfileId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    public static (Match, Match) CreatePair(Guid profileId, Guid matchedProfileId, DateTime matchDateTime)
    {
        var match1 = new Match
        {
            Id = Guid.NewGuid(),
            ProfileId = profileId,
            MatchedProfileId = matchedProfileId,
            MatchDateTime = matchDateTime
        };
        var match2 = new Match
        {
            Id = Guid.NewGuid(),
            ProfileId = matchedProfileId,
            MatchedProfileId = profileId,
            MatchDateTime = matchDateTime
        };

        match1.OppositeMatchId = match2.Id;
        match2.OppositeMatchId = match1.Id;

        return (match1, match2);
    }

    public void SetSwipe(Swipe swipe, Swipe? oppositeMatchSwipe)
    {
        Swipe = swipe;
        SwipeDateTime = DateTime.UtcNow;

        if (oppositeMatchSwipe != Models.Swipe.Right)
            return;

        var domainEvent = new BothSwipedRightDomainEvent(Id, ProfileId, MatchedProfileId);
        AddDomainEvent(domainEvent);
    }

    private Match()
    {
    }
}