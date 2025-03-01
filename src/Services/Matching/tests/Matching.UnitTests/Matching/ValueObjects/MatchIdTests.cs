namespace Matching.UnitTests.Matching.ValueObjects;

public class MatchIdTests
{
    [Fact]
    public void Of_ShouldReturnMatchId_WhenValueIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var result = MatchId.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidMatchIdException_WhenValueIsEmpty()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Action act = () => MatchId.Of(value);

        // Assert
        act.Should().Throw<InvalidMatchIdException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnGuid_WhenMatchIdIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();
        var matchId = MatchId.Of(value);

        // Act
        Guid result = matchId;

        // Assert
        result.Should().Be(value);
    }
}