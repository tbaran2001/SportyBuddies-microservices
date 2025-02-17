namespace ProfileManagement.UnitTests.Fakes.Profiles;

public static class FakeProfileRemoveSport
{
    public static void Generate(Profile profile)
    {
        profile.RemoveSport(
            SportId.Of(Guid.NewGuid()));
    }

    public static void Generate(Profile profile, SportId sportId)
    {
        profile.RemoveSport(sportId);
    }
}