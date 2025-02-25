namespace ProfileManagement.UnitTests.Profiles.Features.Commands.CreateProfile;

public class CreateProfileValidatorTests : ValidatorTestBase<CreateProfileCommand>
{
    protected override CreateProfileCommand CreateValidObject()
    {
        return new FakeCreateProfileCommand().Generate();
    }

    protected override IValidator<CreateProfileCommand> CreateValidator()
    {
        return new CreateProfileCommandValidator();
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
}