namespace ProfileManagement.UnitTests.Fakes.Profiles;

public static class FakeProfileUpdate
{
    public static void Generate(Profile profile)
    {
        profile.Update(
            profile.Name,
            profile.Description,
            profile.BirthDate,
            profile.Gender);
    }
}