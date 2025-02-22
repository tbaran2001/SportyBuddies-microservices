namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class ProfileIdTests
{
    [Fact]
    public void Of_ShouldReturnProfileId_WhenValueIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var result = ProfileId.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidProfileIdException_WhenValueIsEmpty()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Action act = () => ProfileId.Of(value);

        // Assert
        act.Should().Throw<InvalidProfileIdException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnGuid_WhenProfileIdIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();
        var profileId = ProfileId.Of(value);

        // Act
        Guid result = profileId;

        // Assert
        result.Should().Be(value);
    }
}