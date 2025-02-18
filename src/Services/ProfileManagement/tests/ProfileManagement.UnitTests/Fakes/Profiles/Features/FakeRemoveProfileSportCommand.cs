namespace ProfileManagement.UnitTests.Fakes.Profiles.Features;

public sealed class FakeRemoveProfileSportCommand:AutoFaker<RemoveProfileSportCommand>
{
    public FakeRemoveProfileSportCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.SportId, x => x.Random.Guid());
    }
}