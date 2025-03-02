namespace Matching.TestShared.Fakes.Matching.Models;

public static class FakeMatchCreatePair
{
    public static (Match, Match) Generate()
    {
        var command = new FakeCreateMatchesCommand().Generate();

        return Match.CreatePair(
            ProfileId.Of(command.ProfileId),
            ProfileId.Of(command.MatchedProfileId),
            MatchedAt.Of(DateTime.Now)
        );
    }
}