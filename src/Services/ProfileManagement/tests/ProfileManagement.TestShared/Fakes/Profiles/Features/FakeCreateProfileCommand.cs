namespace ProfileManagement.TestShared.Fakes.Profiles.Features;

public sealed class FakeCreateProfileCommand : AutoFaker<CreateProfileCommand>
{
    public FakeCreateProfileCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.Name, x => x.Person.FullName);
        RuleFor(x => x.Description, x => x.Lorem.Sentence());
    }
}