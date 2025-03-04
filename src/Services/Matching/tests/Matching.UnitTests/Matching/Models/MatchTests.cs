namespace Matching.UnitTests.Matching.Models;

public class MatchTests : BaseDomainTest
{
    [Fact]
    public void CreatePair_ShouldCreateMatchPair_WhenValidData()
    {
        // Arrange
        var (fakeMatch1, _) = FakeMatchCreatePair.Generate();

        // Act
        var (match1, match2) =
            Match.CreatePair(fakeMatch1.ProfileId, fakeMatch1.MatchedProfileId, fakeMatch1.MatchedAt);

        // Assert
        match1.Should().NotBeNull();
        match1.ProfileId.Should().Be(fakeMatch1.ProfileId);
        match1.MatchedProfileId.Should().Be(fakeMatch1.MatchedProfileId);
        match1.MatchedAt.Should().Be(fakeMatch1.MatchedAt);
        match1.Swipe.Should().Be(Swipe.Unknown);
        match1.SwipedAt.Should().BeNull();
        match1.DomainEvents.Should().BeEmpty();

        match2.Should().NotBeNull();
        match2.ProfileId.Should().Be(fakeMatch1.MatchedProfileId);
        match2.MatchedProfileId.Should().Be(fakeMatch1.ProfileId);
        match2.MatchedAt.Should().Be(fakeMatch1.MatchedAt);
        match2.Swipe.Should().Be(Swipe.Unknown);
        match2.SwipedAt.Should().BeNull();
        match2.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void SetSwipe_ShouldSetSwipe_WhenValidData()
    {
        // Arrange
        var (match1, _) = FakeMatchCreatePair.Generate();

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
        var (match1, _) = FakeMatchCreatePair.Generate();

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
        var (match1, _) = FakeMatchCreatePair.Generate();

        // Act
        match1.SetSwipe(Swipe.Right, Swipe.Unknown);

        // Assert
        match1.DomainEvents.Should().BeEmpty();
    }
}