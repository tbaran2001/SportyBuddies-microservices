namespace ProfileManagement.UnitTests.Fakes.Sports.Features;

public sealed class FakeCreateSportCommand : AutoFaker<CreateSportCommand>
{
    public FakeCreateSportCommand()
    {
        RuleFor(x => x.SportId, x => x.Random.Guid());
    }
}