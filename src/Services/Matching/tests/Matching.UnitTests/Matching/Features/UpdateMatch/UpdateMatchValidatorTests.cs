using FluentValidation;

namespace Matching.UnitTests.Matching.Features.UpdateMatch;

public class UpdateMatchValidatorTests : ValidatorTestBase<UpdateMatchCommand>
{
    protected override UpdateMatchCommand CreateValidObject()
    {
        return new UpdateMatchCommand(Guid.NewGuid(), Swipe.Right);
    }

    protected override IValidator<UpdateMatchCommand> CreateValidator()
    {
        return new UpdateMatchCommandValidator();
    }

    [Fact]
    protected override void Validate_ShouldPass_WhenAllPropertiesAreValid()
    {
        base.Validate_ShouldPass_WhenAllPropertiesAreValid();
    }

    [Fact]
    public void Validate_ShouldFail_WhenMatchIdIsEmpty()
    {
        // Act
        var result = Validate(x => x with { MatchId = Guid.Empty });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MatchId);
    }

    [Fact]
    public void Validate_ShouldFail_WhenSwipeIsInvalid()
    {
        // Act
        var result = Validate(x => x with { Swipe = (Swipe)100 });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Swipe);
    }
}