namespace ProfileManagement.UnitTests.Profiles.Features.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesValidatorTests : ValidatorTestBase<UpdateProfilePreferencesCommand>
{
    protected override UpdateProfilePreferencesCommand CreateValidObject()
    {
        return new FakeUpdateProfilePreferencesCommand().Generate();
    }

    protected override IValidator<UpdateProfilePreferencesCommand> CreateValidator()
    {
        return new UpdateProfilePreferencesCommandValidator();
    }

    [Fact]
    protected override void Validate_ShouldPass_WhenAllPropertiesAreValid()
    {
        base.Validate_ShouldPass_WhenAllPropertiesAreValid();
    }

    [Fact]
    public void Validate_ShouldFail_WhenProfileIdIsEmpty()
    {
        // Act
        var result = Validate(x => x with { ProfileId = Guid.Empty });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProfileId);
    }

    [Fact]
    public void Validate_ShouldFail_WhenMinAgeIsLessThan18()
    {
        // Act
        var result = Validate(x => x with { MinAge = 17 });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MinAge);
    }

    [Fact]
    public void Validate_ShouldFail_WhenMaxAgeIsMoreThan100()
    {
        // Act
        var result = Validate(x => x with { MaxAge = 101 });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxAge);
    }

    [Theory]
    [InlineData(20, 19)]
    [InlineData(19, 18)]
    public void Validate_ShouldFail_WhenMinAgeIsGreaterThanMaxAge(int minAge, int maxAge)
    {
        // Act
        var result = Validate(x => x with { MinAge = minAge, MaxAge = maxAge });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MinAge);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(101)]
    public void Validate_ShouldFail_WhenMaxDistanceIsInvalid(int maxDistance)
    {
        // Act
        var result = Validate(x => x with { MaxDistance = maxDistance });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxDistance);
    }


    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public void Validate_ShouldFail_WhenPreferredGenderIsInvalid(int gender)
    {
        // Act
        var result = Validate(x => x with { PreferredGender = (Gender)gender });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PreferredGender);
    }
}