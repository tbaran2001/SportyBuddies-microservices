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
}