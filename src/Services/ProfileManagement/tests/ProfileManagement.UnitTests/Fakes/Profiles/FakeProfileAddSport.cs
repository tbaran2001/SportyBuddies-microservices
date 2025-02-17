namespace ProfileManagement.UnitTests.Fakes.Profiles;

public static class FakeProfileAddSport
{
    public static void Generate(Profile profile)
    {
        profile.AddSport(
            SportId.Of(Guid.NewGuid()));
    }

    public static void Generate(Profile profile, SportId sportId)
    {
        profile.AddSport(sportId);
    }
}