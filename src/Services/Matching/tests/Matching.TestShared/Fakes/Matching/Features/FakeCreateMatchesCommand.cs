namespace Matching.TestShared.Fakes.Matching.Features;

public sealed class FakeCreateMatchesCommand : AutoFaker<CreateMatchesCommand>
{
    public FakeCreateMatchesCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.MatchedProfileId, x => x.Random.Guid());
    }
}