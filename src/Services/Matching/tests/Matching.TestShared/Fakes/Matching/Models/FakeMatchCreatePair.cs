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
    
    public static List<Match> Generate(int count)
    {
        var matches = new List<Match>();

        for (var i = 0; i < count; i++)
        {
            var (m1, m2) = Generate();
            matches.Add(m1);
            matches.Add(m2);
        }

        return matches;
    }
}