namespace ProfileManagement.UnitTests.Profiles.Features.Commands.UpdateProfile;

public class UpdateProfileValidatorTests : ValidatorTestBase<UpdateProfileCommand>
{
    protected override UpdateProfileCommand CreateValidObject()
    {
        var fakeProfile = FakeProfileCreate.Generate();
        return new FakeUpdateProfileCommand(fakeProfile.Id).Generate();
    }

    protected override IValidator<UpdateProfileCommand> CreateValidator()
    {
        return new UpdateProfileCommandValidator();
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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_ShouldFail_WhenNameIsEmpty(string name)
    {
        // Act
        var result = Validate(x => x with { Name = name });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_ShouldFail_WhenDescriptionIsEmpty(string description)
    {
        // Act
        var result = Validate(x => x with { Description = description });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public void Validate_ShouldFail_WhenGenderIsInvalid(int gender)
    {
        // Act
        var result = Validate(x => x with { Gender = (Gender)gender });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Gender);
    }

    [Theory]
    [InlineData(17)]
    [InlineData(121)]
    public void Validate_ShouldFail_WhenBirthDateIsInvalid(int age)
    {
        // Act
        var result = Validate(x => x with { BirthDate = DateTime.Now.AddYears(-age) });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BirthDate);
    }
}