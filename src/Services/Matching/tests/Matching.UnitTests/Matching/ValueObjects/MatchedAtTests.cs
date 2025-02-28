namespace Matching.UnitTests.Matching.ValueObjects;

public class MatchedAtTests
{
    [Fact]
    public void Of_ShouldReturnMatchedAt_WhenValueIsValid()
    {
        // Arrange
        var value = DateTime.Now;

        // Act
        var result = MatchedAt.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidMatchedAtException_WhenValueIsDefault()
    {
        // Arrange
        var value = default(DateTime);

        // Act
        Action act = () => MatchedAt.Of(value);

        // Assert
        act.Should().Throw<InvalidMatchedAtException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnDateTime_WhenMatchedAtIsValid()
    {
        // Arrange
        var value = DateTime.Now;
        var matchedAt = MatchedAt.Of(value);

        // Act
        DateTime result = matchedAt;

        // Assert
        result.Should().Be(value);
    }
}