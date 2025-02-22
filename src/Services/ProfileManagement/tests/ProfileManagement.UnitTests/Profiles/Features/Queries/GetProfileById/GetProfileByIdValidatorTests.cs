namespace ProfileManagement.UnitTests.Profiles.Features.Queries.GetProfileById;

public class GetProfileByIdValidatorTests: ValidatorTestBase<GetProfileByIdQuery>
{
    protected override GetProfileByIdQuery CreateValidObject()
    {
        return new GetProfileByIdQuery(Guid.NewGuid());
    }

    protected override IValidator<GetProfileByIdQuery> CreateValidator()
    {
        return new GetProfileByIdQueryValidator();
    }

    [Fact]
    public void Validate_ShouldFail_WhenProfileIdIsEmpty()
    {
        // Act
        var result = Validate(x => x with { Id = Guid.Empty });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}