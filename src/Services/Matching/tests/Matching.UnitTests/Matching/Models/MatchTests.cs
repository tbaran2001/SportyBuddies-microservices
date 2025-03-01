namespace Matching.UnitTests.Matching.Models;

public class MatchTests : BaseDomainTest
{
    [Fact]
    public void CreatePair_ShouldCreateMatchPair_WhenValidData()
    {
        // Arrange
        var profileId = ProfileId.Of(Guid.NewGuid());
        var matchedProfileId = ProfileId.Of(Guid.NewGuid());
        var matchedAt = MatchedAt.Of(DateTime.Now);

        // Act
        var (match1, match2) = Match.CreatePair(profileId, matchedProfileId, matchedAt);

        // Assert
        match1.Should().NotBeNull();
        match1.Id.Value.Should().NotBeEmpty();
        match1.ProfileId.Should().Be(profileId);
        match1.MatchedProfileId.Should().Be(matchedProfileId);
        match1.MatchedAt.Should().Be(matchedAt);
        match1.Swipe.Should().Be(Swipe.Unknown);
        match1.SwipedAt.Should().BeNull();
        match1.OppositeMatchId.Should().Be(match2.Id);

        match2.Should().NotBeNull();
        match2.Id.Value.Should().NotBeEmpty();
        match2.ProfileId.Should().Be(matchedProfileId);
        match2.MatchedProfileId.Should().Be(profileId);
        match2.MatchedAt.Should().Be(matchedAt);
        match2.Swipe.Should().Be(Swipe.Unknown);
        match2.SwipedAt.Should().BeNull();
        match2.OppositeMatchId.Should().Be(match1.Id);
    }

    [Fact]
    public void SetSwipe_ShouldSetSwipe_WhenValidData()
    {
        // Arrange
        var profileId = ProfileId.Of(Guid.NewGuid());
        var matchedProfileId = ProfileId.Of(Guid.NewGuid());
        var matchedAt = MatchedAt.Of(DateTime.Now);

        var (match1, _) = Match.CreatePair(profileId, matchedProfileId, matchedAt);

        // Act
        match1.SetSwipe(Swipe.Right, Swipe.Left);

        // Assert
        match1.Swipe.Should().Be(Swipe.Right);
        match1.SwipedAt.Should().NotBeNull();
    }

    [Fact]
    public void SetSwipe_ShouldPublishBothSwipedRightDomainEvent_WhenBothSwipedRight()
    {
        // Arrange
        var profileId = ProfileId.Of(Guid.NewGuid());
        var matchedProfileId = ProfileId.Of(Guid.NewGuid());
        var matchedAt = MatchedAt.Of(DateTime.Now);

        var (match1, _) = Match.CreatePair(profileId, matchedProfileId, matchedAt);

        // Act
        match1.SetSwipe(Swipe.Right, Swipe.Right);

        // Assert
        var domainEvent = AssertDomainEventWasPublished<BothSwipedRightDomainEvent>(match1);
        domainEvent.MatchId.Should().Be(match1.Id);
        domainEvent.ProfileId.Should().Be(match1.ProfileId);
        domainEvent.MatchedProfileId.Should().Be(match1.MatchedProfileId);
    }

    [Fact]
    public void SetSwipe_ShouldNotPublishBothSwipedRightDomainEvent_WhenNotBothSwipedRight()
    {
        // Arrange
        var profileId = ProfileId.Of(Guid.NewGuid());
        var matchedProfileId = ProfileId.Of(Guid.NewGuid());
        var matchedAt = MatchedAt.Of(DateTime.Now);

        var (match1, _) = Match.CreatePair(profileId, matchedProfileId, matchedAt);

        // Act
        match1.SetSwipe(Swipe.Right, Swipe.Unknown);

        // Assert
        match1.DomainEvents.Should().BeEmpty();
    }
}