namespace ProfileManagement.UnitTests.Fakes.Profiles;

public static class FakeProfileUpdatePreferences
{
    public static void Generate(Profile profile)
    {
        profile.UpdatePreferences(Preferences.Default);
    }
}