namespace ProfileManagement.UnitTests.Fakes.Profiles.Features;

public sealed class FakeAddProfileSportCommand : AutoFaker<AddProfileSportCommand>
{
    public FakeAddProfileSportCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.SportId, x => x.Random.Guid());
    }
}