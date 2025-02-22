namespace ProfileManagement.UnitTests.Profiles.Features.Commands.RemoveProfileSport;

public class RemoveProfileSportValidatorTests : ValidatorTestBase<RemoveProfileSportCommand>
{
    protected override RemoveProfileSportCommand CreateValidObject()
    {
        return new FakeRemoveProfileSportCommand().Generate();
    }

    protected override IValidator<RemoveProfileSportCommand> CreateValidator()
    {
        return new RemoveProfileSportCommandValidator();
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