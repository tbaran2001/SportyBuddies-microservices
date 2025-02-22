using FluentValidation;
using FluentValidation.TestHelper;

namespace BuildingBlocks.Test;

public abstract class ValidatorTestBase<TModel>
{
    protected abstract TModel CreateValidObject();

    protected TestValidationResult<TModel> Validate(Func<TModel, TModel> modify)
    {
        var model = CreateValidObject();
        var modifiedModel = modify(model);

        var validator = CreateValidator();

        return validator.TestValidate(modifiedModel);
    }

    protected abstract IValidator<TModel> CreateValidator();

    protected virtual void Validate_ShouldPass_WhenAllPropertiesAreValid()
    {
        // Arrange
        var command = CreateValidObject();

        // Act
        var result = Validate(x => x); // No modifications

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}