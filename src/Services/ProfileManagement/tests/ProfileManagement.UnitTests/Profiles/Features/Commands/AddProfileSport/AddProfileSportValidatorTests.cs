namespace ProfileManagement.UnitTests.Profiles.Features.Commands.AddProfileSport;

public class AddProfileSportValidatorTests : ValidatorTestBase<AddProfileSportCommand>
{
    protected override AddProfileSportCommand CreateValidObject()
    {
        return new FakeAddProfileSportCommand().Generate();
    }

    protected override IValidator<AddProfileSportCommand> CreateValidator()
    {
        return new AddProfileSportCommandValidator();
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
    public void Validate_ShouldFail_WhenSportIdIsEmpty()
    {
        // Act
        var result = Validate(x => x with { SportId = Guid.Empty });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SportId);
    }
}