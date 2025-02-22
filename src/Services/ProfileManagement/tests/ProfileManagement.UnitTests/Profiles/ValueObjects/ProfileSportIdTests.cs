namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class ProfileSportIdTests
{
    [Fact]
    public void Of_ShouldReturnProfileSportId_WhenValueIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var result = ProfileSportId.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidProfileSportIdException_WhenValueIsEmpty()
    {
        // Arrange
        var value = Guid.Empty;

        // Act
        Action act = () => ProfileSportId.Of(value);

        // Assert
        act.Should().Throw<InvalidProfileSportIdException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnGuid_WhenProfileSportIdIsValid()
    {
        // Arrange
        var value = Guid.NewGuid();
        var profileSportId = ProfileSportId.Of(value);

        // Act
        Guid result = profileSportId;

        // Assert
        result.Should().Be(value);
    }
}