namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Of_ShouldReturnDescription_WhenValueIsValid()
    {
        // Arrange
        var value = "Description";

        // Act
        var result = Description.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Of_ShouldThrowInvalidDescriptionException_WhenValueIsInvalid(string value)
    {
        // Act
        Action act = () => Description.Of(value);

        // Assert
        act.Should().Throw<InvalidDescriptionException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnString_WhenDescriptionIsValid()
    {
        // Arrange
        var value = "Description";
        var description = Description.Of(value);

        // Act
        string result = description;

        // Assert
        result.Should().Be(value);
    }
}