namespace ProfileManagement.UnitTests.Fakes.Profiles.Features;

public sealed class FakeUpdateProfileCommand : AutoFaker<UpdateProfileCommand>
{
    public FakeUpdateProfileCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.Name, x => x.Person.FullName);
        RuleFor(x => x.Description, x => x.Lorem.Sentence());
        RuleFor(x => x.Gender, x => (Gender)x.Random.Int(0, 2));
        RuleFor(x => x.BirthDate, x => DateOnly.FromDateTime(x.Date.Past()));
    }
}