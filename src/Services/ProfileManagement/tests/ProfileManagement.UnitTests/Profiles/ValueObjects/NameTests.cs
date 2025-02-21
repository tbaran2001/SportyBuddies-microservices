namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class NameTests
{
    [Fact]
    public void Of_ShouldReturnName_WhenValueIsValid()
    {
        // Arrange
        var value = "Name";

        // Act
        var result = Name.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Of_ShouldThrowInvalidNameException_WhenValueIsInvalid(string value)
    {
        // Act
        Action act = () => Name.Of(value);

        // Assert
        act.Should().Throw<InvalidNameException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnString_WhenNameIsValid()
    {
        // Arrange
        var value = "Name";
        var name = Name.Of(value);

        // Act
        string result = name;

        // Assert
        result.Should().Be(value);
    }
}