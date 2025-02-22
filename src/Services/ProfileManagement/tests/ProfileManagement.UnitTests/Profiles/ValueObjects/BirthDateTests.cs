namespace ProfileManagement.UnitTests.Profiles.ValueObjects;

public class BirthDateTests
{
    [Fact]
    public void Of_ShouldReturnBirthDate_WhenValueIsValid()
    {
        // Arrange
        var value = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

        // Act
        var result = BirthDate.Of(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Of_ShouldThrowInvalidBirthDateException_WhenValueIsDefault()
    {
        // Arrange
        var value = default(DateOnly);

        // Act
        Action act = () => BirthDate.Of(value);

        // Assert
        act.Should().Throw<InvalidBirthDateException>();
    }

    [Theory]
    [InlineData(17)]
    [InlineData(120)]
    public void Of_ShouldThrowInvalidBirthDateException_WhenValueIsInvalid(int years)
    {
        // Arrange
        var value = DateOnly.FromDateTime(DateTime.Now.AddYears(-years));

        // Act
        Action act = () => BirthDate.Of(value);

        // Assert
        act.Should().Throw<InvalidBirthDateException>();
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnDateOnly_WhenBirthDateIsValid()
    {
        // Arrange
        var value = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
        var birthDate = BirthDate.Of(value);

        // Act
        DateOnly result = birthDate;

        // Assert
        result.Should().Be(value);
    }
}