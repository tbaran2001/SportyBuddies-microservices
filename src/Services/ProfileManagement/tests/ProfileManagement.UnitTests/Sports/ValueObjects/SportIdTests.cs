namespace ProfileManagement.UnitTests.Sports.ValueObjects;

public class SportIdTests
{
    [Fact]
    public void Of_ShouldReturnSportId_WhenValueIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var result = SportId.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidSportIdException_WhenValueIsEmpty()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Action act = () => SportId.Of(value);

        // Assert
        act.Should().Throw<InvalidSportIdException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnGuid_WhenSportIdIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();
        var sportId = SportId.Of(value);

        // Act
        Guid result = sportId;

        // Assert
        result.Should().Be(value);
    }
}