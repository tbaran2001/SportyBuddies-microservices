namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class PreferencesTests
{
    [Fact]
    public void Of_ShouldReturnPreferences_WhenValueIsValid()
    {
        // Arrange
        int minAge = 20, maxAge = 40, maxDistance = 30;
        Gender preferredGender = Gender.Female;

        // Act
        var result = Preferences.Of(minAge, maxAge, maxDistance, preferredGender);

        // Assert
        result.Should().NotBeNull();
        result.MinAge.Should().Be(minAge);
        result.MaxAge.Should().Be(maxAge);
        result.MaxDistance.Should().Be(maxDistance);
        result.PreferredGender.Should().Be(preferredGender);
    }

    [Theory]
    [InlineData(-1, 40, 50, (Gender)2)]
    [InlineData(20, -40, 50, (Gender)2)]
    [InlineData(20, 40, 0, (Gender)2)]
    public void Of_ShouldThrowDomainException_WhenValueIsInvalid(int minAge, int maxAge, int maxDistance,
        Gender preferredGender)
    {
        // Act
        Action act = () => Preferences.Of(minAge, maxAge, maxDistance, preferredGender);

        // Assert
        act.Should().Throw<DomainException>();
    }
}