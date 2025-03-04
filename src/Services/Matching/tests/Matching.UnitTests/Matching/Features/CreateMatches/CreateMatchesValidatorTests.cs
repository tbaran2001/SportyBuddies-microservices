using FluentValidation;

namespace Matching.UnitTests.Matching.Features.CreateMatches;

public class CreateMatchesValidatorTests : ValidatorTestBase<CreateMatchesCommand>
{
    protected override CreateMatchesCommand CreateValidObject()
    {
        return new FakeCreateMatchesCommand().Generate();
    }

    protected override IValidator<CreateMatchesCommand> CreateValidator()
    {
        return new CreateMatchesCommandValidator();
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
    public void Validate_ShouldFail_WhenMatchedProfileIdIsEmpty()
    {
        // Act
        var result = Validate(x => x with { MatchedProfileId = Guid.Empty });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MatchedProfileId);
    }
}