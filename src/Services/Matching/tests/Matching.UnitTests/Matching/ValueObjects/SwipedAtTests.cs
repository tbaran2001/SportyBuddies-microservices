namespace Matching.UnitTests.Matching.ValueObjects;

public class SwipedAtTests
{
    [Fact]
    public void Of_ShouldReturnSwipedAt_WhenValueIsValid()
    {
        // Arrange
        var value = DateTime.Now;

        // Act
        var result = SwipedAt.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidSwipedAtException_WhenValueIsDefault()
    {
        // Arrange
        var value = default(DateTime);

        // Act
        Action act = () => SwipedAt.Of(value);

        // Assert
        act.Should().Throw<InvalidSwipedAtException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnDateTime_WhenSwipedAtIsValid()
    {
        // Arrange
        var value = DateTime.Now;
        var swipedAt = SwipedAt.Of(value);

        // Act
        DateTime result = swipedAt;

        // Assert
        result.Should().Be(value);
    }
}